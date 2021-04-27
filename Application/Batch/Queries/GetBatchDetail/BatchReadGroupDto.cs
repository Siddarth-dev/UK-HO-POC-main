using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.Batch.Queries.GetBatchDetail
{
    public class BatchReadGroupDto : IMapFrom<ReadGroup>
    {
        public string ReadGroup { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ReadGroup, BatchReadGroupDto>()
                .ForMember(d => d.ReadGroup, opts => opts.MapFrom(s => s.GroupName));
        }
    }
}