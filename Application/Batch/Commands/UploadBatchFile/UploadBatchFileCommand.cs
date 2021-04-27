using System;
using MediatR;

namespace Application.Batch.Commands.UploadBatchFile
{
    public class UploadBatchFileCommand : IRequest
    {
        public Guid BatchId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string Content { get; set; }
        public long FileSize { get; set; }
        public string MimeType { get; set; }
    }
}