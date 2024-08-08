using Core.Entities.Common;

namespace Core.Entities;

public class CourseCategory : BaseEntity
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public ICollection<Course>? Courses { get; set; }
}
