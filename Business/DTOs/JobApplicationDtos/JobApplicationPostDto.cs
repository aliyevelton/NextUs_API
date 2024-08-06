using Microsoft.AspNetCore.Http;

namespace Business.DTOs.JobApplicationDtos;

public class JobApplicationPostDto
{
    public string FullName { get; set; } = null!;
    public string? CoverLetter { get; set; }
    public IFormFile Cv { get; set; } = null!;
    public int JobId { get; set; }
    public string UserId { get; set; }
}
