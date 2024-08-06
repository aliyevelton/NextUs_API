using AutoMapper;
using Business.DTOs.JobDtos;
using Business.Helpers.Enums;
using Core.Entities;

namespace Business.MappingProfiles;

public class JobMapper : Profile
{
    public JobMapper()
    {
        CreateMap<JobGetDto, Job>().ReverseMap();

        CreateMap<Job, JobDetailDto>()
            .ForMember(x => x.JobType, y => y.MapFrom(src => ConvertJobTypeToDisplayString((JobTypes)src.JobType)))
            .ReverseMap()
            .ForMember(x => x.JobType, y => y.MapFrom(src => (int)Enum.Parse<JobTypes>(src.JobType)));


        CreateMap<JobPostDto, Job>()
            .ForMember(x => x.IsActive, y => y.MapFrom(x => true))
            .ForMember(x => x.IsDeleted, y => y.MapFrom(x => false))
            .ForMember(x => x.ExpireDate, y => y.MapFrom(x => DateTime.UtcNow + TimeSpan.FromDays(40)))
            .ForMember(x => x.IsFeatured, y => y.MapFrom(x => false))
            .ForMember(x => x.IsPremium, y => y.MapFrom(x => false))
            .ForMember(x => x.IsApproved, y => y.MapFrom(x => false))
            .ForMember(x => x.ExactSalary, y => y.Condition(src => src.SalaryType != SalaryType.NotSpecified))
            .ForMember(x => x.MinSalary, y => y.Condition(src => src.SalaryType == SalaryType.Range))
            .ForMember(x => x.MaxSalary, y => y.Condition(src => src.SalaryType == SalaryType.Range))
            .ForMember(x => x.SalaryType, y => y.MapFrom(src => src.SalaryType))
            .ReverseMap();

        CreateMap<JobPutDto, Job>()
            .ForMember(dest => dest.ExactSalary, opt => opt.Ignore())
            .ForMember(dest => dest.MinSalary, opt => opt.Ignore())
            .ForMember(dest => dest.MaxSalary, opt => opt.Ignore())
            .AfterMap((src, dest) =>
            {
                if (src.SalaryType == SalaryType.Exact)
                {
                    dest.ExactSalary = src.ExactSalary;
                    dest.MinSalary = null;
                    dest.MaxSalary = null;
                }
                else if (src.SalaryType == SalaryType.Range)
                {
                    dest.ExactSalary = null;
                    dest.MinSalary = src.MinSalary;
                    dest.MaxSalary = src.MaxSalary;
                }
                else if (src.SalaryType == SalaryType.NotSpecified)
                {
                    dest.ExactSalary = null;
                    dest.MinSalary = null;
                    dest.MaxSalary = null;
                }
            })
            .ReverseMap();
    }

    public string ConvertJobTypeToDisplayString(JobTypes jobType)
    {
        return jobType switch
        {
            JobTypes.FullTime => "Full Time",
            JobTypes.PartTime => "Part Time",
            JobTypes.Remote => "Remote",
            JobTypes.Freelance => "Freelance",
            JobTypes.Internship => "Internship",
            _ => "Unknown"
        };
    }

}
