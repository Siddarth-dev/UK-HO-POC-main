using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class BatchAttributeConfiguration : IEntityTypeConfiguration<BatchAttribute>
    {
        public void Configure(EntityTypeBuilder<BatchAttribute> builder)
        {
            builder.HasOne(e => e.Batch).WithMany(c => c.BatchAttributes)
            .HasForeignKey(e => e.BatchId);
        }
    }
}