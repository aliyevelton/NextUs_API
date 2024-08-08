using AutoMapper;
using Business.DTOs.CourseCategoryDtos;
using Core.Entities;

namespace Business.MappingProfiles;

public class CourseCategoryMapper : Profile
{
    public CourseCategoryMapper()
    {
        CreateMap<CourseCategoryPostDto, CourseCategory>().ReverseMap();
        CreateMap<CourseCategoryGetDto, CourseCategory>().ReverseMap();
        CreateMap<CourseCategoryPutDto, CourseCategory>().ReverseMap();
    }
}
