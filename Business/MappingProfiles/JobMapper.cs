using AutoMapper;
using Business.DTOs.JobDtos;
using Core.Entities;

namespace Business.MappingProfiles;

public class JobMapper : Profile
{
    public JobMapper()
    {
        CreateMap<JobGetDto, Job>().ReverseMap();

        CreateMap<JobPostDto, Job>()
            .ForMember(x => x.IsActive, y => y.MapFrom(x => true))
            .ForMember(x => x.IsDeleted, y => y.MapFrom(x => false))
            .ForMember(x => x.ExpireDate, y => y.MapFrom(x => DateTime.UtcNow + TimeSpan.FromDays(40)))
            .ForMember(x => x.IsFeatured, y => y.MapFrom(x => false))
            .ForMember(x => x.IsPremium, y => y.MapFrom(x => false))
            .ForMember(x => x.IsApproved, y => y.MapFrom(x => false))
            .ReverseMap();

        CreateMap<JobPutDto, Job>().ReverseMap();
    }
}
