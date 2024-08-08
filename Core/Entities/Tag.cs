using Core.Entities.Common;

namespace Core.Entities;

public class Tag : BaseEntity
{
    public string Name { get; set; } = null!;
    public ICollection<CourseTag>? Courses { get; set; }
    public ICollection<JobTag>? Jobs { get; set; }
}
