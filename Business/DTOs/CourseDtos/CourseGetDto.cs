using Business.DTOs.CompanyDtos;

namespace Business.DTOs.CourseDtos;

public class CourseGetDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Position { get; set; } = null!;
    public CompanyGetDto Company { get; set; }
}
