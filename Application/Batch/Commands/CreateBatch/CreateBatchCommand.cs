using System;
using System.Collections.Generic;
using Application.Batch.Queries.GetBatchDetail;
using MediatR;

namespace Application.Batch.Commands.CreateBatch
{
    public class CreateBatchCommand : IRequest<Guid>
    {
        public string BusinessUnit { get; set; }
        public BatchAclModel Acl { get; set; }
        public List<BatchAttributeDetailModel> Attributes { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}