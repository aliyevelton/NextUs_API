namespace Business.DTOs.CourseBookmarkDtos;

public class CourseBookmarkDto
{
    public int Id { get; set; }
    public int CourseId { get; set; }
    public string UserId { get; set; } = null!;
}
