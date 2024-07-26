using Business.DTOs.AuthDtos;
using Business.DTOs.MailDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Business.Services.Interfaces;

public interface IAuthService
{
    Task<TokenResponseDto> LoginAsync(LoginDto loginDto);
    Task<ConfirmEmailResponseDto> ConfirmEmailAsync(ConfirmEmailDto confirmEmailDto);
    Task GeneratePasswordResetLinkAsync(string email, HttpRequest request);
    Task<IdentityResult> ResetPasswordAsync(ConfirmResetPasswordDto confirmResetPasswordDto);
    Task<bool> ValidateResetTokenAsync(string email, string token);
}
