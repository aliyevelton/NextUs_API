using AutoMapper;
using Business.DTOs.CourseBookmarkDtos;
using Business.DTOs.CourseDtos;
using Business.DTOs.CompanyDtos;
using Core.Entities;

namespace Business.MappingProfiles
{
    public class CourseBookmarkMapper : Profile
    {
        public CourseBookmarkMapper()
        {
            // Mapping between CourseBookmarkPostDto and CourseBookmark
            CreateMap<CourseBookmarkPostDto, CourseBookmark>()
                .ForMember(x => x.CreatedDate, y => y.MapFrom(src => DateTime.UtcNow))
                .ReverseMap();

            CreateMap<CourseBookmark, CourseBookmarkDto>()
                .ForMember(x => x.UserId, y => y.MapFrom(src => src.User.Id))
                .ReverseMap();

            // Mapping between CourseBookmark and CourseGetDto
            CreateMap<CourseBookmark, CourseGetDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Course.Id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Course.Title))
                .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Course.Company))
                .ReverseMap();

            // Mapping between Course and CourseGetDto
            CreateMap<Course, CourseGetDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company));

            // Mapping between Company and CompanyGetDto
            CreateMap<Company, CompanyGetDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Logo, opt => opt.MapFrom(src => src.Logo));
        }
    }
}
