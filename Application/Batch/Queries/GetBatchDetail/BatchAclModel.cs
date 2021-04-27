using System.Collections.Generic;
using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Batch.Queries.GetBatchDetail
{
    public class BatchAclModel
    {
        public List<string> ReadUsers { get; set; }
        public List<string> ReadGroups { get; set; }
    }
}