using AutoMapper;
using Business.DTOs.UserDtos;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Business.MappingProfiles; 

public class UserMapper : Profile
{
    public UserMapper()
    {
        CreateMap<UserCreateDto, AppUser>()
            .ForMember(x => x.IsActive, y => y.MapFrom(x => true))
            .ReverseMap();

        CreateMap<AppUser, UserDetailDto>()
            .ForMember(x => x.Roles, y => y.Ignore())
            .ReverseMap();

        CreateMap<AppUser, UserGetDto>()
        .ForMember(dest => dest.Roles, opt => opt.MapFrom((src, dest, destMember, context) =>
        ((UserManager<AppUser>)context.Items["UserManager"]).GetRolesAsync(src).Result))
        .ReverseMap();
        CreateMap<IdentityRole, RoleDto>().ReverseMap();
    }
}
