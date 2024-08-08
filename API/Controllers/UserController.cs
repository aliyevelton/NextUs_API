using Business.DTOs.MailDtos;
using Business.DTOs.UserDtos;
using Business.HelperServices;
using Business.Services.Interfaces;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Security.Claims;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly UserManager<AppUser> _userManager;
    private readonly IConfiguration _configuration;

    public UserController(IUserService userService, IConfiguration configuration, UserManager<AppUser> userManager)
    {
        _userService = userService;
        _configuration = configuration;
        _userManager = userManager;
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Register(UserCreateDto user)
    {
        await _userService.CreateUserAsync(user);

        var newUser = await _userManager.FindByEmailAsync(user.Email);

        string token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);

        //string link = Url.Action("ConfirmEmail", "Auth", new { email = newUser.Email, token }, HttpContext.Request.Scheme, HttpContext.Request.Host.Value);

        string baseUrl = "http://localhost:5173/login";
        string encodedToken = Uri.EscapeDataString(token);
        string link = $"{baseUrl}?email={newUser.Email}&token={encodedToken}";

        string body = $"Please confirm your email by clicking <a href='{link}'>HERE</a>.";

        EmailHelper emailHelper = new EmailHelper(_configuration);
        await emailHelper.SendEmailAsync(new MailRequest { ToEmail = newUser.Email, Subject = "Confirm Email", Body = body });

        return StatusCode((int)HttpStatusCode.Created, $"User is successfully created. Email: {user.Email}. Please confirm your email to login your account.");
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> FindUser(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user == null)
            return NotFound("User not found");

        return Ok($"User email: {user.Email}");
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> FindUserById(string userId)
    {
        var user = await _userService.FindUserById(userId);

        if (user == null)
            return NotFound("User not found");

        return Ok(user);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> DeleteUser(string email)
    {
        await _userService.DeleteUser(email);
        return StatusCode((int)HttpStatusCode.NoContent);
    }

    [HttpGet]
    //[Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userService.GetAllUsers();
        return Ok(users);
    }

    [HttpGet("current")]
    [Authorize]
    public async Task<IActionResult> GetUserDetails()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; // Get user ID from claims
        if (userId == null)
            return Unauthorized();

        var user = await _userService.FindUser(userId);

        return Ok(user);
    }

    [HttpPut("deactivate")]
    //[Authorize]
    public async Task<IActionResult> DeactivateUser(string userId)
    {
        await _userService.DeactivateUserAsync(userId);

        return StatusCode((int)HttpStatusCode.NoContent);
    }

    [HttpPut("activate")]
    //[Authorize]
    public async Task<IActionResult> ActivateUser(string userId)
    {
        await _userService.ActivateUserAsync(userId);

        return StatusCode((int)HttpStatusCode.NoContent);
    }

    [HttpPut("update-profile-image")]
    [Authorize]
    public async Task<IActionResult> UpdateProfilePhoto([FromForm] UpdateProfilePhotoDto updateProfilePhotoDto)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; 
        if (userId == null)
            return Unauthorized();

        await _userService.UpdateProfilePhotoAsync(userId, updateProfilePhotoDto);

        return StatusCode((int)HttpStatusCode.NoContent);
    }

    [HttpPut("update-roles")]
    //[Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateRoles(ChangeUserRolesDto changeUserRolesDto)
    {
        await _userService.UpdateUserRolesAsync(changeUserRolesDto);

        return StatusCode((int)HttpStatusCode.NoContent);
    }

    [HttpGet("all-roles")]
    //[Authorize(Roles = "Admin")]
    public async Task<IActionResult> AllRoles()
    {
        var roles = await _userService.AllRoles();
        return Ok(roles);
    }

    [HttpPut("update-cover-image")]
    [Authorize]
    public async Task<IActionResult> UpdateCoverImage([FromForm] UpdateCoverImageDto updateCoverImageDto)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
            return Unauthorized();

        await _userService.UpdateCoverImageAsync(userId, updateCoverImageDto);

        return StatusCode((int)HttpStatusCode.NoContent);
    }

    [HttpPut("update-about-text")]
    [Authorize]
    public async Task<IActionResult> UpdateAboutText([FromBody] UpdateAboutTextDto updateAboutTextDto)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
            return Unauthorized();

        await _userService.UpdateAboutTextAsync(userId, updateAboutTextDto);

        return StatusCode((int)HttpStatusCode.NoContent);
    }
}
