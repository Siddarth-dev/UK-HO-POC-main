using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class BatchAclConfiguration: IEntityTypeConfiguration<Acl>
    {
        public void Configure(EntityTypeBuilder<Acl> builder)
        {
            builder.HasMany(e => e.ReadGroups).WithOne(c => c.Acl).HasForeignKey(e => e.Id);
            builder.HasMany(e => e.ReadUsers).WithOne(c => c.Acl).HasForeignKey(e => e.Id);
        }
    }
}