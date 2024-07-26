using AutoMapper;
using Business.DTOs.ContactUsDtos;
using Core.Entities;

namespace Business.MappingProfiles;

public class ContactUsMapper : Profile
{
    public ContactUsMapper()
    {
        CreateMap<ContactUsDto, ContactUs>().ReverseMap();
    }
}
