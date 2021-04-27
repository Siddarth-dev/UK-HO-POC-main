using System;
using MediatR;

namespace Application.Batch.Queries.GetBatchDetail
{
    public class BatchDetailQuery: IRequest<BatchDetailModel>
    {
        public Guid BatchId { get; set; }
    }
}