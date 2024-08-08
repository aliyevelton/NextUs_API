using AutoMapper;
using Business.DTOs.AuthDtos;
using Business.DTOs.UserDtos;
using Business.Exceptions.UserExceptions;
using Business.Helpers.Enums;
using Business.HelperServices.Interfaces;
using Business.Services.Interfaces;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Business.Services.Implementations;

public class UserService : IUserService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IMapper _mapper;
    private readonly IFileService _fileService;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UserService(UserManager<AppUser> userManager, IMapper mapper, RoleManager<IdentityRole> roleManager, IFileService fileService)
    {
        _userManager = userManager;
        _mapper = mapper;
        _roleManager = roleManager;
        _fileService = fileService;
    }

    public async Task CreateUserAsync(UserCreateDto userCreateDto)
    {
        var existingUser = await _userManager.FindByEmailAsync(userCreateDto.Email);
        if (existingUser != null)
            throw new UserAlreadyExistsException("User already exists with this email");
        AppUser user = _mapper.Map<AppUser>(userCreateDto);

        IdentityResult identityResult = await _userManager.CreateAsync(user, userCreateDto.Password);
        if (!identityResult.Succeeded) 
            throw new CreateFailedException(identityResult.Errors);

        await _userManager.AddToRoleAsync(user, Roles.User.ToString());
    }

    public async Task<UserDetailDto> FindUser(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            throw new UserNotFoundByEmailException("User not found");


        var roles = await _userManager.GetRolesAsync(user)  ;

        var userDto = _mapper.Map<UserDetailDto>(user);

        userDto.Roles = roles.ToList();

        return userDto;
    }

    public async Task<UserDetailDto> FindUserById(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            throw new UserNotFoundException($"User not found by Id: {userId}");

        var roles = _userManager.GetRolesAsync(user);

        var userDto = _mapper.Map<UserDetailDto>(user);

        userDto.Roles = roles.Result.ToList();

        return userDto;
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

    public async Task DeactivateUserAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            throw new UserNotFoundByEmailException("User not found");

        user.IsActive = false;
        await _userManager.UpdateAsync(user);
    }

    public async Task ActivateUserAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            throw new UserNotFoundByEmailException("User not found");

        user.IsActive = true;
        await _userManager.UpdateAsync(user);
    }

    public async Task<List<RoleDto>> AllRoles()
    {
        var roles = await _roleManager.Roles.ToListAsync();
        if (roles == null)
            throw new RoleNotFoundException("Roles not found");

        var roleDtos = _mapper.Map<List<RoleDto>>(roles);
        return roleDtos;
    }

    public async Task<List<UserGetDto>> GetAllUsers()
    {
        var users = await _userManager.Users.ToListAsync();
        if (users == null)
            throw new UserNotFoundException("Users not found");

        var userGetDtos = _mapper.Map<List<UserGetDto>>(users, opt => opt.Items["UserManager"] = _userManager);

        return userGetDtos;
    }

    public async Task UpdateUserRolesAsync(ChangeUserRolesDto changeUserRolesDto)
    {
        var user = await _userManager.FindByIdAsync(changeUserRolesDto.UserId);
        if (user == null)
            throw new UserNotFoundByEmailException("User not found");

        var userRoles = await _userManager.GetRolesAsync(user);

        var selectedRoles = changeUserRolesDto.Roles ?? new List<string>();

        if (!selectedRoles.Any())
        {
            // Assign the "User" role if no roles are selected
            selectedRoles.Add("User");
        }

        var result = await _userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles));
        if (!result.Succeeded)
            throw new UpdateRolesFailedException(result.Errors);

        result = await _userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles));
        if (!result.Succeeded)
            throw new UpdateRolesFailedException(result.Errors);
    }

    public async Task UpdateProfilePhotoAsync(string userId, UpdateProfilePhotoDto updateProfilePhotoDto)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            throw new UserNotFoundByEmailException("User not found");

        if (updateProfilePhotoDto.ProfilePhotoFile != null && user.ProfilePhoto != null)
        {
            _fileService.DeleteAsync("images", "user-images", user.ProfilePhoto);

            string fileName = await _fileService.UploadFileAsync(updateProfilePhotoDto.ProfilePhotoFile, "image/", 5000, "images", "user-images");

            user.ProfilePhoto = fileName;
        } else if (updateProfilePhotoDto.ProfilePhotoFile != null && user.ProfilePhoto == null)
        {
            string fileName = await _fileService.UploadFileAsync(updateProfilePhotoDto.ProfilePhotoFile, "image/", 5000, "images", "user-images");

            user.ProfilePhoto = fileName;
        } else if (updateProfilePhotoDto.ProfilePhotoFile == null && user.ProfilePhoto != null)
        {
            _fileService.DeleteAsync("images", "user-images", user.ProfilePhoto);

            user.ProfilePhoto = null;
        }

        await _userManager.UpdateAsync(user);
    }

    public async Task UpdateCoverImageAsync(string userId, UpdateCoverImageDto updateCoverImageDto)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            throw new UserNotFoundByEmailException("User not found");

        if (updateCoverImageDto.CoverImageFile != null && user.CoverImage != null)
        {
            _fileService.DeleteAsync("images", "user-images", user.CoverImage);

            string fileName = await _fileService.UploadFileAsync(updateCoverImageDto.CoverImageFile, "image/", 5000, "images", "user-images");

            user.CoverImage = fileName;
        } else if (updateCoverImageDto.CoverImageFile != null && user.CoverImage == null)
        {
            string fileName = await _fileService.UploadFileAsync(updateCoverImageDto.CoverImageFile, "image/", 5000, "images", "user-images");

            user.CoverImage = fileName;
        } else if (updateCoverImageDto.CoverImageFile == null && user.CoverImage != null)
        {
            _fileService.DeleteAsync("images", "user-images", user.CoverImage);

            user.CoverImage = null;
        }

        await _userManager.UpdateAsync(user);
    }

    public async Task UpdateAboutTextAsync(string userId, UpdateAboutTextDto updateAboutTextDto)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            throw new UserNotFoundByEmailException("User not found");

        user.About = updateAboutTextDto.AboutText;
        await _userManager.UpdateAsync(user);
    }
}
