using System;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Test.Common
{
    public class DataContextFactory
    {
        public static DataContext Create()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new DataContext(options);

            context.Database.EnsureCreated();

            context.BusinessUnities.AddRange(
                new Domain.Entities.BusinessUnit{ Id = 1, BusinessUnitName = "Demo0 BU", IsActive = true},
                new Domain.Entities.BusinessUnit{ Id = 2, BusinessUnitName = "Demo1 BU", IsActive = true},
                new Domain.Entities.BusinessUnit{ Id = 3, BusinessUnitName = "Demo2 BU", IsActive = true}
                );

            context.BatchStatus.AddRange(
                new Domain.Entities.BatchStatus{ Id = 1, Status = "InProcess", IsActive = true},
                new Domain.Entities.BatchStatus{ Id = 2, Status = "Complete", IsActive = true},
                new Domain.Entities.BatchStatus{ Id = 3, Status = "InComplete", IsActive = true}
            );

            /*context.ReadGroups.AddRange(
                new Domain.Entities.ReadGroup{ Id = 1, GroupName = "GroupName01", Acl = new Domain.Entities.Acl{ Id = 1, BatchId = new Guid("187E4DA7-12E2-49B1-C9ED-08D8F297BB6D") }},
                new Domain.Entities.ReadGroup{ Id = 2, GroupName = "GroupName02" , Acl = new Domain.Entities.Acl{ Id = 2, BatchId = new Guid("03D24A2D-5090-4008-CC71-08D8F295E9E2") }},
                new Domain.Entities.ReadGroup{ Id = 3, GroupName = "GroupName03", Acl = new Domain.Entities.Acl{ Id = 3, BatchId = new Guid("5DE84209-D78F-4A21-4F2B-08D8F01C7E5D") }}
            );

            context.ReadUsers.AddRange(
                new Domain.Entities.ReadUser{ Id = 1, UserName = "UserName1", Acl = new Domain.Entities.Acl{ Id = 1, BatchId = new Guid("187E4DA7-12E2-49B1-C9ED-08D8F297BB6D") }},
                new Domain.Entities.ReadUser{ Id = 2, UserName = "UserName2", Acl = new Domain.Entities.Acl{ Id = 2, BatchId = new Guid("03D24A2D-5090-4008-CC71-08D8F295E9E2") }},
                new Domain.Entities.ReadUser{ Id = 3, UserName = "UserName3", Acl = new Domain.Entities.Acl{ Id = 3, BatchId = new Guid("5DE84209-D78F-4A21-4F2B-08D8F01C7E5D") }}
            );*/

            context.Acls.AddRange(
                new Domain.Entities.Acl{Id = 1, BatchId = new Guid("187E4DA7-12E2-49B1-C9ED-08D8F297BB6D"), IsActive = true},
                new Domain.Entities.Acl{Id = 2, BatchId = new Guid("03D24A2D-5090-4008-CC71-08D8F295E9E2"), IsActive = true},
                new Domain.Entities.Acl{Id = 3, BatchId = new Guid("5DE84209-D78F-4A21-4F2B-08D8F01C7E5D"), IsActive = true}
            );

            context.BatchAttributes.AddRange(
                new Domain.Entities.BatchAttribute{ Id = 1, BatchId = new Guid("187E4DA7-12E2-49B1-C9ED-08D8F297BB6D"), IsActive = true},
                new Domain.Entities.BatchAttribute{ Id = 2, BatchId = new Guid("03D24A2D-5090-4008-CC71-08D8F295E9E2"), IsActive = true},
                new Domain.Entities.BatchAttribute{ Id = 3, BatchId = new Guid("5DE84209-D78F-4A21-4F2B-08D8F01C7E5D"), IsActive = true}
            );

            context.Batches.AddRange(
                new Domain.Entities.Batch{ Id= new Guid("187E4DA7-12E2-49B1-C9ED-08D8F297BB6D"), IsActive = true, BatchStatusId = 1, BusinessUnitId = 1, BatchPublishedDate = DateTime.Now, ExpiryDate = DateTime.Now.AddMonths(2)},
                new Domain.Entities.Batch{ Id= new Guid("03D24A2D-5090-4008-CC71-08D8F295E9E2"), IsActive = true, BatchStatusId = 2, BusinessUnitId = 2, BatchPublishedDate = DateTime.Now, ExpiryDate = DateTime.Now.AddMonths(2)},
                new Domain.Entities.Batch{ Id= new Guid("5DE84209-D78F-4A21-4F2B-08D8F01C7E5D"), IsActive = true, BatchStatusId = 3, BusinessUnitId = 3, BatchPublishedDate = DateTime.Now, ExpiryDate = DateTime.Now.AddMonths(2)}
            );

            context.SaveChanges();

            return context;
        }

        public static void Destroy(DataContext context)
        {
            context.Database.EnsureDeleted();

            context.Dispose();
        }
    }
}