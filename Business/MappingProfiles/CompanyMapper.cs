using AutoMapper;
using Business.DTOs.CompanyDtos;
using Business.DTOs.CompanyDTOs;
using Core.Entities;

namespace Business.MappingProfiles;

public class CompanyMapper : Profile
{
    public CompanyMapper()
    {
        CreateMap<CompanyPostDto, Company>().ReverseMap();
        CreateMap<CompanyGetDto, Company>().ReverseMap();
    }
}
