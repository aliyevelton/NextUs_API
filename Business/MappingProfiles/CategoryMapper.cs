using AutoMapper;
using Business.DTOs.CategoryDtos;
using Core.Entities;

namespace Business.MappingProfiles;

public class CategoryMapper : Profile
{
    public CategoryMapper()
    {
        CreateMap<JobCategoryPostDto, JobCategory>().ReverseMap();
        CreateMap<JobCategoryGetDto, JobCategory>().ReverseMap();
    }
}
