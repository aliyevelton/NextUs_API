using Business.DTOs.UserDtos;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Business.Services.Interfaces;

public interface IUserService
{
    Task CreateUserAsync(UserCreateDto userCreateDto);
    Task<UserDetailDto> FindUser(string email);
    Task<UserDetailDto> FindUserById(string userId);
    Task DeleteUser(string email);
    Task<List<UserGetDto>> GetAllUsers();
    Task DeactivateUserAsync(string userId);
    Task ActivateUserAsync(string userId);
    Task<List<RoleDto>> AllRoles();
    Task UpdateUserRolesAsync(ChangeUserRolesDto changeUserRolesDto);
    Task UpdateProfilePhotoAsync(string userId, UpdateProfilePhotoDto updateProfilePhotoDto);
    Task UpdateCoverImageAsync(string userId, UpdateCoverImageDto updateCoverImageDto);
    Task UpdateAboutTextAsync(string userId, UpdateAboutTextDto updateAboutTextDto);
}
