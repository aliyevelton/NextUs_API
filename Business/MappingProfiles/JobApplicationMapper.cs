using AutoMapper;
using Business.DTOs.CompanyDtos;
using Business.DTOs.JobApplicationDtos;
using Business.DTOs.JobDtos;
using Core.Entities;

namespace Business.MappingProfiles
{
    public class JobApplicationMapper : Profile
    {
        public JobApplicationMapper()
        {
            CreateMap<JobApplicationPostDto, JobApplication>().ReverseMap();

            CreateMap<JobApplication, JobApplicationGetDto>()
                .ForMember(dest => dest.CoverLetter, opt => opt.MapFrom(src => src.CoverLetter))
                .ForMember(dest => dest.Cv, opt => opt.MapFrom(src => src.Cv))
                .ForMember(dest => dest.JobId, opt => opt.MapFrom(src => src.Job.Id))
                .ForMember(dest => dest.Job, opt => opt.MapFrom(src => src.Job))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ReverseMap();

            CreateMap<Job, JobGetDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.Position))
                .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company));

            CreateMap<Company, CompanyGetDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Logo, opt => opt.MapFrom(src => src.Logo));
        }
    }
}
