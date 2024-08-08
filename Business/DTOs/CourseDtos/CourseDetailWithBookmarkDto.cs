namespace Business.DTOs.CourseDtos;

public class CourseDetailWithBookmarkDto
{
    public CourseDetailDto CourseDetail { get; set; }
    public bool IsBookmarked { get; set; }
}
