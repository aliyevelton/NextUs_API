using AutoMapper;
using Business.DTOs.CourseBookmarkDtos;
using Core.Entities;

namespace Business.MappingProfiles;

public class CourseBookmarkMapper : Profile
{
    public CourseBookmarkMapper()
    {
        CreateMap<CourseBookmarkPostDto, CourseBookmark>()
            .ForMember(x => x.CreatedDate, y => y.MapFrom(src => DateTime.UtcNow))
            .ReverseMap();

        CreateMap<CourseBookmark, CourseBookmarkDto>()
            .ForMember(x => x.UserId, y => y.MapFrom(src => src.User.Id))
            .ReverseMap();
    }
}
