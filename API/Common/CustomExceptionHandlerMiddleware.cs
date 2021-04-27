using System;
using System.Net;
using System.Threading.Tasks;
using Application.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

namespace API.Common
{
        public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        
        private readonly CorrelationIdOptions _options;

        public CustomExceptionHandlerMiddleware(RequestDelegate next, IOptions<CorrelationIdOptions> options)
        {
            _next = next;
            _options = options.Value;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                if (context.Request.Headers.TryGetValue(_options.Header, out StringValues correlationId))
                {
                    context.TraceIdentifier = correlationId;
                }
                if (_options.IncludeInResponse)
                {
                    // apply the correlation ID to the response header for client side tracking
                    context.Response.OnStarting(() =>
                    {
                        context.Response.Headers.Add(_options.Header, new[] { context.TraceIdentifier });
                        return Task.CompletedTask;
                    });
                }
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;
            //Console.WriteLine($"CorrelationId/TraceIdentifier Id {context.TraceIdentifier}");
            var result = string.Empty;

            switch (exception)
            {
                case ValidationException validationException:
                    code = HttpStatusCode.BadRequest;
                    result = JsonConvert.SerializeObject(validationException.Failures);
                    break;
                case BadRequestException badRequestException:
                    code = HttpStatusCode.BadRequest;
                    result = badRequestException.Message;
                    break;
                case NotFoundException _:
                    code = HttpStatusCode.NotFound;
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            if (result == string.Empty)
            {
                result = JsonConvert.SerializeObject(new { error = exception.Message });
            }

            return context.Response.WriteAsync(result);
        }
    }

    public static class CustomExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
        }
    }
}