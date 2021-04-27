using System;
using System.Collections.Generic;
using Application.Common.Mappings;
using AutoMapper;

namespace Application.Batch.Queries.GetBatchDetail
{
    public class BatchDetailModel: IMapFrom<Domain.Entities.Batch>
    {
        public Guid BatchId { get; set; }
        public string Status { get; set; }
        public List<BatchAttributeDto> Attributes { get; set; }
        public string BusinessUnit { get; set; }
        public BatchAclDto Acl { get; set; }
        public DateTime BatchPublishedDate { get; set; }
        public DateTime ExpiryDate { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.Batch, BatchDetailModel>()
                .ForMember(d => d.BatchId, opts => opts.MapFrom(s => s.Id));
        }
    }
}