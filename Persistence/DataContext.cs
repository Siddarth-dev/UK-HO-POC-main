using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Batch> Batches { get; set; }
        public DbSet<BusinessUnit> BusinessUnities { get; set; }
        public DbSet<BatchAttribute> BatchAttributes { get; set; }
        public DbSet<BatchStatus> BatchStatus { get; set; }
        public DbSet<Acl> Acls { get; set; }
        public DbSet<ReadGroup> ReadGroups { get; set; }
        public DbSet<ReadUser> ReadUsers { get; set; }
        public DbSet<UploadBatchFile> UploadBatchFiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
        }
    }
}