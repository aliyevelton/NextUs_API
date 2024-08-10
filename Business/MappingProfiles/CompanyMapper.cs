using AutoMapper;
using Business.DTOs.CompanyDtos;
using Business.DTOs.CompanyDTOs;
using Business.DTOs.CourseDtos;
using Business.DTOs.JobDtos;
using Core.Entities;

namespace Business.MappingProfiles;

public class CompanyMapper : Profile
{
    public CompanyMapper()
    {
        CreateMap<CompanyPostDto, Company>().ReverseMap();
        CreateMap<CompanyGetDto, Company>()
            .ForMember(x => x.Jobs, y => y.MapFrom(src => src.Jobs))
            .ForMember(c => c.Courses, y => y.MapFrom(src => src.Courses))
            .ReverseMap();
        CreateMap<Job, JobSummaryDto>().ReverseMap();
        CreateMap<Course, CourseSummaryDto>().ReverseMap();

    }
}
