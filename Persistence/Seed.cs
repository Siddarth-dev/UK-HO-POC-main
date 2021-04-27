using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Persistence
{
    public class Seed
    {
        public static void SeedData(DataContext context)
        {
            if (!context.BatchStatus.Any())
            {
                var batchStatus = new List<BatchStatus>() {
                    
                    new BatchStatus() {
                        Status = "InProcess",
                        IsActive = true
                    },
                    new BatchStatus() {
                        Status = "Complete",
                        IsActive = true
                    },
                    new BatchStatus() {
                        Status = "InComplete",
                        IsActive = true
                    },
                    new BatchStatus() {
                        Status = "Abort",
                        IsActive = true
                    }
                };
                context.BatchStatus.AddRange(batchStatus);
                context.SaveChanges();
            }
            if (!context.BusinessUnities.Any())
            {
                var businessUnities = new List<BusinessUnit>() {
                    new BusinessUnit() {
                        BusinessUnitName = "Demo0 BU",
                        IsActive = true
                    },
                    new BusinessUnit() {
                        BusinessUnitName = "Demo1 BU",
                        IsActive = true
                    },
                    new BusinessUnit() {
                        BusinessUnitName = "Demo2 BU",
                        IsActive = true
                    }
                };
                context.BusinessUnities.AddRange(businessUnities);
                context.SaveChanges();
            }
        }
    }
}