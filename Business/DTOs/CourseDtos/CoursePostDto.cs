using Microsoft.AspNetCore.Http;

namespace Business.DTOs.CourseDtos;

public class CoursePostDto
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int? Price { get; set; }
    public int CategoryId { get; set; }
    public int CompanyId { get; set; }
    public int TotalHours { get; set; }
    public IFormFile? Syllabus { get; set; }
    public string Location { get; set; } = null!;
    public int CourseType { get; set; }
    public List<string>? Tags { get; set; }
}
