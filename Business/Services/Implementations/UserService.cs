using AutoMapper;
using Business.DTOs.AuthDtos;
using Business.DTOs.UserDtos;
using Business.Exceptions.UserExceptions;
using Business.Helpers.Enums;
using Business.Services.Interfaces;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Business.Services.Implementations;

public class UserService : IUserService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IMapper _mapper;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UserService(UserManager<AppUser> userManager, IMapper mapper, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _mapper = mapper;
        _roleManager = roleManager;
    }

    public async Task CreateUserAsync(UserCreateDto userCreateDto)
    {
        AppUser user = _mapper.Map<AppUser>(userCreateDto);

        user.UserName = userCreateDto.Email;

        IdentityResult identityResult = await _userManager.CreateAsync(user, userCreateDto.Password);
        if (!identityResult.Succeeded) 
            throw new CreateFailedException(identityResult.Errors);

        await _userManager.AddToRoleAsync(user, Roles.User.ToString());
    }

    public Task FindUser(string email)
    {
        var user = _userManager.FindByEmailAsync(email);
        if (user == null)
            throw new UserNotFoundByEmailException("User not found");

        return user;
    }

    public async Task DeleteUser(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
            throw new UserNotFoundByEmailException("User not found");

        IdentityResult identityResult = await _userManager.DeleteAsync(user);
        if (!identityResult.Succeeded)
            throw new DeleteFailedException("Couldn't delete user");
    }

    public async Task<List<AppUser>> GetAllUsers()
    {
        var users = await _userManager.Users.ToListAsync();
        if (users == null)
            throw new UserNotFoundException("Users not found");

        return users;
    }
}
