using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Persistence;

namespace Application.Batch.Commands.UploadBatchFile
{
    public class UploadBatchFileCommandHandler : IRequestHandler<UploadBatchFileCommand>
    {
        private readonly DataContext _context;
        private readonly IMediator _mediator;
        public UploadBatchFileCommandHandler(DataContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(UploadBatchFileCommand request, CancellationToken cancellationToken)
        {
            var entity = new Domain.Entities.UploadBatchFile
            {
                BatchId = request.BatchId,
                ContainerName = request.BatchId.ToString().ToLowerInvariant(),
                FileName = request.FileName,
                FileSize = request.FileSize,
                MimeType = request.MimeType,
                Created = DateTime.Now,
                CreatedBy = string.Empty,
                LastModified = DateTime.Now,
                LastModifiedBy = string.Empty
            };

            await _context.UploadBatchFiles.AddAsync(entity);
            /**
             * * Used for Local
            */ 
            // var path = @"~..\..\..\Infrastructure\UploadTestFiles\Test.json";//For local
            /**
             * * Used for Azure
            */ 
            var path = System.IO.Directory.GetCurrentDirectory() + @"\Test.json";//For Azure
            // var path = @"F:\Siddarth\Hydrographic\batchdemo\Infrastructure\UploadTestFiles\Test.json";

            using var fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            using var sr = new StreamReader(fs, Encoding.UTF8);
            
            var uploadBatchFileToContainer = new UploadBatchFileToContainer {
                ContainerName = entity.ContainerName,
                Content = sr.ReadToEnd(),
                FilePath = path,
                FileName = entity.FileName,
                FileSize = entity.FileSize,
                MimeType = entity.MimeType
            };
            await _mediator.Publish(uploadBatchFileToContainer, cancellationToken);
            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}