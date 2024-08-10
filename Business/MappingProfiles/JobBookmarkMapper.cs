using AutoMapper;
using Business.DTOs.CompanyDtos;
using Business.DTOs.JobBookmarkDtos;
using Business.DTOs.JobDtos;
using Core.Entities;

namespace Business.MappingProfiles;

public class JobBookmarkMapper : Profile
{
    public JobBookmarkMapper()
    {
        // Mapping between JobBookmarkPostDto and JobBookmark
        CreateMap<JobBookmarkPostDto, JobBookmark>()
            .ForMember(x => x.CreatedDate, y => y.MapFrom(src => DateTime.UtcNow))
            .ReverseMap();

        CreateMap<JobBookmark, JobBookmarkDto>()
            .ForMember(x => x.UserId, y => y.MapFrom(src => src.User.Id))
            .ReverseMap();

        CreateMap<JobBookmark, JobGetDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Job.Id))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Job.Title))
            .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.Job.Position))
            .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Job.Company))
            .ReverseMap();

        CreateMap<Job, JobGetDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.Position))
            .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company));

        CreateMap<Company, CompanyGetDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
    }
}
