using Business.DTOs.CompanyDtos;

namespace Business.DTOs.CourseDtos;

public class CourseDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string CompanyName { get; set; } = null!;
    public string? CompanyLogo { get; set; } 
}
