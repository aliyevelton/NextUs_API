using Business.Helpers.Enums;

namespace Business.DTOs.JobDtos;

public class JobPostDto
{
    public string Title { get; set; } = null!;
    public string Position { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Location { get; set; } = null!;
    public int JobType { get; set; }
    public int? ExactSalary { get; set; }
    public int? MinSalary { get; set; }
    public int? MaxSalary { get; set; }
    public SalaryType SalaryType { get; set; }
    public int CategoryId { get; set; }
    public int CompanyId { get; set; }
    public List<string>? Tags { get; set; } 
}
