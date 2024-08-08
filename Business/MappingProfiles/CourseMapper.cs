using AutoMapper;
using Business.DTOs.CourseDtos;
using Business.Helpers.Enums;
using Core.Entities;

namespace Business.MappingProfiles;

public class CourseMapper : Profile
{
    public CourseMapper()
    {
        CreateMap<CourseGetDto, Course>().ReverseMap();

        CreateMap<Course, CourseDetailDto>()
            .ForMember(x => x.CourseType, y => y.MapFrom(src => ConvertCourseTypeToDisplayString((CourseTypes)src.CourseType)))
            .ReverseMap()
            .ForMember(x => x.CourseType, y => y.MapFrom(src => (int)Enum.Parse<CourseTypes>(src.CourseType)));

        CreateMap<CoursePostDto, Course>()
            .ForMember(x => x.IsActive, y => y.MapFrom(x => true))
            .ForMember(x => x.IsDeleted, y => y.MapFrom(x => false))
            .ForMember(x => x.IsApproved, y => y.MapFrom(x => false))
            .ForMember(x => x.IsDeleted, y => y.MapFrom(x => false))
            .ForMember(x => x.IsApproved, y => y.MapFrom(x => false))
            .ForMember(x => x.Tags, y => y.Ignore())
            .ReverseMap();

        CreateMap<CoursePutDto, Course>()
            .ReverseMap();
    }

    public string ConvertCourseTypeToDisplayString(CourseTypes courseType)
    {
        return courseType switch
        {
            CourseTypes.Offline => "Offline",
            CourseTypes.Online => "Online",
            CourseTypes.Hybrid => "Hybrid",
            _ => "Unknown"
        };
    }
}
