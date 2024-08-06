    using AutoMapper;
using Business.DTOs.JobApplicationDtos;
using Core.Entities;

namespace Business.MappingProfiles;

public class JobApplicationMapper : Profile
{
    public JobApplicationMapper()
    {
        CreateMap<JobApplicationPostDto, JobApplication>().ReverseMap();
    }
}
