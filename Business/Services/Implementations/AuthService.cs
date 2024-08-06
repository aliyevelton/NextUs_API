using Business.DTOs.AuthDtos;
using Business.DTOs.MailDtos;
using Business.Exceptions.AuthExceptions;
using Business.Exceptions.UserExceptions;
using Business.HelperServices;
using Business.HelperServices.Interfaces;
using Business.Services.Interfaces;
using Core.Entities.Identity;
using DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Net;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Business.Services.Implementations;

public class AuthService : IAuthService
{
    private readonly SignInManager<AppUser> _signInManager;
    private readonly UserManager<AppUser> _userManager;
    private readonly ITokenService _tokenService;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IConfiguration _configuration;

    //private readonly IRepository<AppUser> _userRepository;

    public AuthService(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, ITokenService tokenService, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _tokenService = tokenService;
        _webHostEnvironment = webHostEnvironment;
        _configuration = configuration;
    }

    public async Task<TokenResponseDto> LoginAsync(LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);
        if (user == null)
            throw new LoginFailedException("Invalid email or password");

        if (!user.EmailConfirmed)
            throw new EmailConfirmationException("Your email is not confirmed. Please confirm your email before you login");

        if (!user.IsActive)
            throw new UserNotActiveException("Your account is not active. Please contact the administrator");

        if (user.LockoutEnd.HasValue && user.LockoutEnd.Value > DateTimeOffset.UtcNow)
            throw new UserLockedException($"Your account is locked out as you had too many failed attempts. Please try again after {user.LockoutEnd.Value.LocalDateTime}");

        SignInResult signInResult = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, true);
        if (!signInResult.Succeeded)
            throw new LoginFailedException("Invalid email or password");

        TokenResponseDto tokenResponseDto = _tokenService.GenerateToken(user);

        user.RefreshToken = tokenResponseDto.RefreshToken;
        user.RefreshTokenExpiryTime = tokenResponseDto.RefreshTokenExpiryTime;
        await _userManager.UpdateAsync(user);

        return tokenResponseDto;
    }

    public async Task<ConfirmEmailResponseDto> ConfirmEmailAsync(ConfirmEmailDto confirmEmailDto)
    {
        //confirmEmailDto.Token.ThrowIfNullOrWhiteSpace(message: "Token cannot be null");
        //confirmEmailDto.Email.ThrowIfNullOrWhiteSpace(message: "Email cannot be null");

        var user = await _userManager.FindByEmailAsync(confirmEmailDto.Email);
        if (user is null)
            throw new UserNotFoundException($"User not found by email: {confirmEmailDto.Email}", HttpStatusCode.BadRequest);

        if (await _userManager.IsEmailConfirmedAsync(user))
            throw new EmailConfirmationException("This account is already activated");

        var result = await _userManager.ConfirmEmailAsync(user, confirmEmailDto.Token);
        if (!result.Succeeded)
            throw new EmailConfirmationException(result.Errors);

        return new(true, $"User is successfully activated. Email: {user.Email}");
    }

    public async Task GeneratePasswordResetLinkAsync(string email, HttpRequest request)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            throw new ArgumentException("User not found");
        }

        string token = await _userManager.GeneratePasswordResetTokenAsync(user);
        string scheme = request.Scheme;
        string host = request.Host.Value;
        string link = $"{scheme}://{host}/api/auth/resetpassword?email={user.Email}&token={Uri.EscapeDataString(token)}";

        string path = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "templates", "index.html");
        using StreamReader streamReader = new StreamReader(path);
        string content = await streamReader.ReadToEndAsync();
        string body = content.Replace("[link]", link);

        EmailHelper emailHelper = new EmailHelper(_configuration);
        await emailHelper.SendEmailAsync(new MailRequest { ToEmail = email, Subject = "Reset Password", Body = body });
    }

    public async Task<bool> ValidateResetTokenAsync(string email, string token)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            return false;
        }

        return await _userManager.VerifyUserTokenAsync(user, TokenOptions.DefaultProvider, "ResetPassword", token);
    }

    public async Task<IdentityResult> ResetPasswordAsync(ConfirmResetPasswordDto confirmResetPasswordDto)
    {
        var user = await _userManager.FindByEmailAsync(confirmResetPasswordDto.Email);
        if (user == null)
        {
            return IdentityResult.Failed(new IdentityError { Description = "User not found" });
        }

        return await _userManager.ResetPasswordAsync(user, confirmResetPasswordDto.Token, confirmResetPasswordDto.NewPassword);
    }
}