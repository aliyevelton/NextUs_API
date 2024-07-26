using Business.DTOs.AuthDtos;
using Business.DTOs.MailDtos;
using Business.Exceptions.AuthExceptions;
using Business.HelperServices.Interfaces;
using Business.Services.Interfaces;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly UserManager<AppUser> _userManager;
    private readonly IAuthService _passwordResetService;
    private readonly IConfiguration _configuration;
    private readonly ITokenService _tokenService; //sersvise cixar burani
    //private readonly RoleManager<IdentityRole> _roleManager;

    public AuthController(IAuthService authService, UserManager<AppUser> userManager, IAuthService passwordResetService /*RoleManager<IdentityRole> roleManager*/)
    {
        _authService = authService;
        _userManager = userManager;
        _passwordResetService = passwordResetService;
        //_roleManager = roleManager;
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        var tokenResponse = await _authService.LoginAsync(loginDto);
        return Ok(tokenResponse);
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] TokenRequestDto tokenRequest)
    {
        var principal = GetPrincipalFromExpiredToken(tokenRequest.AccessToken);
        var username = principal.Identity.Name;

        var user = await _userManager.FindByNameAsync(username);
        if (user == null || user.RefreshToken != tokenRequest.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            return BadRequest("Invalid client request");

        var newAccessToken = _tokenService.GenerateAccessToken(principal.Claims);
        var newRefreshToken = _tokenService.GenerateRefreshToken();
        var newRefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7); // Set new refresh token expiry time

        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpiryTime = newRefreshTokenExpiryTime;
        await _userManager.UpdateAsync(user);

        return new ObjectResult(new TokenResponseDto
        {
            Token = newAccessToken,
            ExpirationDate = DateTime.UtcNow.AddHours(3),
            RefreshToken = newRefreshToken,
            RefreshTokenExpiryTime = newRefreshTokenExpiryTime
        });
    }

    private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecurityKey"])),
            ValidateLifetime = false // Allow expired tokens
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
        if (securityToken is JwtSecurityToken jwtSecurityToken &&
            jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        {
            return principal;
        }

        throw new SecurityTokenException("Invalid token");
    }


    [HttpPost("[action]")]
    public async Task<IActionResult> ConfirmEmail([FromQuery] ConfirmEmailDto confirmEmailDto)
    {
        var confirmEmailResponse = await _authService.ConfirmEmailAsync(confirmEmailDto);

        return Ok(confirmEmailResponse);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> GeneratePasswordResetLink([FromBody] ResetPasswordDto resetPasswordDto)
    {
        var request = HttpContext.Request;
        await _authService.GeneratePasswordResetLinkAsync(resetPasswordDto.Email, request);

        return Ok("Reset password link has been sent");
    }

    [HttpPost("resetpassword")]
    public async Task<IActionResult> ResetPassword([FromQuery] ResetPasswordDto resetPasswordDto)
    {
        var isValid = await _passwordResetService.ValidateResetTokenAsync(resetPasswordDto.Email, resetPasswordDto.Token);
        if (isValid)
        {
            return Ok("Token is valid.");
        }
        else
        {
            return BadRequest("Invalid token.");
        }
    }

    [HttpPost("reset-password-confirm")]
    public async Task<IActionResult> ResetPasswordConfirm([FromQuery] string email, [FromQuery] string token, [FromBody] ResetPasswordRequestDto passwordRequestDto)
    {
        var confirmResetPasswordDto = new ConfirmResetPasswordDto
        {
            Email = email,
            Token = token,
            NewPassword = passwordRequestDto.NewPassword,
        };
        var result = await _passwordResetService.ResetPasswordAsync(confirmResetPasswordDto);
        if (result.Succeeded)
        {
            return Ok("Password has been reset successfully.");
        }
        else
        {
            return BadRequest(result.Errors);
        }
    }

    //[HttpPost("add-roles")]
    //public async Task<IActionResult> AddRoles([FromBody] string role)
    //{
    //    if (!await _roleManager.RoleExistsAsync(role))
    //    {
    //        var result = await _roleManager.CreateAsync(new IdentityRole(role));
    //        if (result.Succeeded)
    //        {
    //            return Ok("Role added successfully.");
    //        }
    //        return BadRequest(result.Errors);
    //    }
    //    return BadRequest("Role already exists.");
    //}
}
