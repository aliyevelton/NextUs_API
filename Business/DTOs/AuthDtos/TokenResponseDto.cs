namespace Business.DTOs.AuthDtos;

public class TokenResponseDto
{
    public string Token { get; set; } = null!;
    public DateTime ExpirationDate { get; set; }
    public string RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
}
