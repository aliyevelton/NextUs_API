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
    public async Task<IActionResult> FindUser(string username)
    {
        var user = await _userManager.FindByNameAsync(username);

        if (user == null)
            return NotFound("User not found");

        return Ok($"User email: {user.Email}");
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> DeleteUser(string email)
    {
        await _userService.DeleteUser(email);
        return StatusCode((int)HttpStatusCode.NoContent);
    }

    [HttpGet]
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

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return NotFound();

        var userDetails = new
        {
            user.Id,
            user.Name,
            user.Surname,
            user.Email,
            user.EmailConfirmed,
            user.ProfilePhoto
        };

        return Ok(userDetails);
    }
}
