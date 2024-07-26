namespace Business.DTOs.AuthDtos;

public class ResetPasswordDto
{
    public string Email { get; set; } = null!;
    public string Token { get; set; } = null!;
}
