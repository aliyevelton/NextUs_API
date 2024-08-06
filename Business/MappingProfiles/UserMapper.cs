using AutoMapper;
using Business.DTOs.UserDtos;
using Core.Entities.Identity;

namespace Business.MappingProfiles; 

public class UserMapper : Profile
{
    public UserMapper()
    {
        CreateMap<UserCreateDto, AppUser>()
            .ForMember(x => x.IsActive, y => y.MapFrom(x => true))
            .ReverseMap();
    }
}
