using Business.DTOs.AuthDtos;
using Core.Entities.Identity;
using System.Security.Claims;

namespace Business.HelperServices.Interfaces;

public interface ITokenService
{
    TokenResponseDto GenerateToken(AppUser appUser);
    string GenerateAccessToken(IEnumerable<Claim> claims);
    string GenerateRefreshToken();
}
