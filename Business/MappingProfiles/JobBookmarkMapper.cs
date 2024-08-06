using AutoMapper;
using Business.DTOs.JobBookmarkDtos;
using Core.Entities;

namespace Business.MappingProfiles;

public class JobBookmarkMapper : Profile
{
    public JobBookmarkMapper()
    {
        CreateMap<JobBookmarkPostDto, JobBookmark>()
            .ForMember(x => x.CreatedDate, y => y.MapFrom(src => DateTime.UtcNow))
            .ReverseMap();

        CreateMap<JobBookmark, JobBookmarkDto>()
            .ForMember(x => x.UserId, y => y.MapFrom(src => src.User.Id))
            .ReverseMap();
    }
}
