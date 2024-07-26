using Microsoft.AspNetCore.Http;

namespace Business.DTOs.CompanyDtos;

public class CompanyGetDto
{
    public string Name { get; set; } = null!;
    public string? About { get; set; }
    public string? Logo { get; set; }
    public string? CoverImage { get; set; }
    public string? Website { get; set; }
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
}
