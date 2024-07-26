using Business.DTOs.UserDtos;
using Core.Entities.Identity;

namespace Business.Services.Interfaces;

public interface IUserService
{
    Task CreateUserAsync(UserCreateDto userCreateDto);
    Task FindUser(string email);
    Task DeleteUser(string email);
    Task<List<AppUser>> GetAllUsers();
}
