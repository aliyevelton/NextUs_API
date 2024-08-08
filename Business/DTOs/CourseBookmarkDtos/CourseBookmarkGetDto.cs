using Business.DTOs.CourseDtos;

namespace Business.DTOs.CourseBookmarkDtos;

public class CourseBookmarkGetDto
{
    public int Id { get; set; }
    public CourseDto Course { get; set; } = null!;
    public string UserId { get; set; } = null!;
}
