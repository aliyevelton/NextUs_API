namespace Business.DTOs.JobDtos;

public class JobPostDto
{
    public string Title { get; set; } = null!;
    public string Position { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Location { get; set; } = null!;
    public string JobType { get; set; } = null!;
    public int Salary { get; set; }
    public int CategoryId { get; set; }
    public int CompanyId { get; set; }
}
