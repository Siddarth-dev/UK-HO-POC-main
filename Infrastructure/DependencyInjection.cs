using Application.Common.Interfaces;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(x => new BlobServiceClient(configuration.GetValue<string>("AzureBlobStorageConnectionString")));
            services.AddSingleton<IAzureBlobStorageService, AzureBlobStorageService>();

            return services;
        }
    }
}