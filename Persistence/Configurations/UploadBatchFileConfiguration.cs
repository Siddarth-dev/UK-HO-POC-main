using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class UploadBatchFileConfiguration : IEntityTypeConfiguration<UploadBatchFile>
    {
        public void Configure(EntityTypeBuilder<UploadBatchFile> builder)
        {
            builder.Property(e => e.Id).HasColumnName("UploadBatchFileId");

            builder.Property(e => e.BatchId)
                .HasColumnName("BatchID");
            
            builder.Property(e => e.ContainerName).HasMaxLength(40);
            builder.Property(e => e.FileName).HasMaxLength(100);
            builder.Property(e => e.FileSize);
            builder.Property(e => e.MimeType).HasMaxLength(100);

        }
    }
}