using Core.Entities.Identity;
using Core.Entities;
using Business.DTOs.JobDtos;

namespace Business.DTOs.JobApplicationDtos;

public class JobApplicationGetDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = null!;
    public string? CoverLetter { get; set; }
    public string Cv { get; set; } = null!;
    public int JobId { get; set; }
    public JobGetDto Job { get; set; }
    public string UserId { get; set; }  
}
