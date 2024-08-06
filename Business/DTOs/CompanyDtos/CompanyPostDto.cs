using Microsoft.AspNetCore.Http;

namespace Business.DTOs.CompanyDTOs;

public class CompanyPostDto
{
    public string Name { get; set; } = null!;
    public string? About { get; set; }
    public IFormFile? LogoFile { get; set; }
    public IFormFile? CoverImageFile { get; set; }
    public string? Website { get; set; }
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
}
