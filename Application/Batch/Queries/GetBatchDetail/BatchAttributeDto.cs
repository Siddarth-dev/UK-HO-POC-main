using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.Batch.Queries.GetBatchDetail
{
    public class BatchAttributeDto: IMapFrom<BatchAttribute>
    {
        public string Key { get; set; }
        public string Value { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<BatchAttribute, BatchAttributeDto>()
                .ForMember(d => d.Key, opts => opts.MapFrom(s => s.Key))
                .ForMember(d => d.Value, opts => opts.MapFrom(s => s.Value));
        }
    }
}