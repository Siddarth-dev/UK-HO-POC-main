using System;
using Domain.Common;

namespace Domain.Entities
{
    public class UploadBatchFile : AuditableEntity
    {
        public int Id { get; set; }
        public Guid BatchId { get; set; }
        public string ContainerName { get; set; }
        public string FileName { get; set; }
        public long FileSize { get; set; }
        public string MimeType { get; set; }
    }
}