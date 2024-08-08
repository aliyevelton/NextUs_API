namespace Business.DTOs.CourseDtos;

public class CourseDetailDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int? Price { get; set; }
    public int CategoryId { get; set; }
    public int CompanyId { get; set; }
    public int TotalHours { get; set; }
    public string? Syllabus { get; set; }
    public string Location { get; set; } = null!;
    public string CourseType { get; set; }
    public List<string>? Tags { get; set; }
}
