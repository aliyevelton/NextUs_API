namespace Business.DTOs.CourseDtos;

public class CourseDetailWithBookmarkDto
{
    public CourseDetailDto Detail { get; set; }
    public bool IsBookmarked { get; set; }
}
