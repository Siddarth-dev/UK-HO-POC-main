using System.Collections.Generic;
using Application.Common.Mappings;
using AutoMapper;

namespace Application.Batch.Queries.GetBatchDetail
{
    public class BatchAclDto: IMapFrom<Domain.Entities.Acl>
    {
        public List<BatchReadUserDto> ReadUsers { get; set; }
        public List<BatchReadGroupDto> ReadGroups { get; set; }
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.ReadGroup, BatchReadGroupDto>()
                .ForMember(d => d.ReadGroup, opts => opts.MapFrom(s => s.GroupName));
            profile.CreateMap<Domain.Entities.ReadUser, BatchReadUserDto>()
                .ForMember(d => d.ReadUser, opts => opts.MapFrom(s => s.UserName));
            profile.CreateMap<Domain.Entities.Acl, BatchAclDto>()
                .ForMember(d => d.ReadGroups, opts => opts.MapFrom(s => s.ReadGroups))
                .ForMember(d => d.ReadUsers, opts => opts.MapFrom(s => s.ReadUsers));
        }
    }
}