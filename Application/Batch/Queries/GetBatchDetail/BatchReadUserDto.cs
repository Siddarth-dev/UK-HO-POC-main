using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.Batch.Queries.GetBatchDetail
{
    public class BatchReadUserDto : IMapFrom<ReadUser>
    {
        public string ReadUser { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ReadUser, BatchReadUserDto>()
                .ForMember(d => d.ReadUser, opts => opts.MapFrom(s => s.UserName));
        }
    }
}