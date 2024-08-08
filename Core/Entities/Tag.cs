using Core.Entities.Common;

namespace Core.Entities;

public class Tag : BaseEntity
{
    public string Name { get; set; } = null!;
    public ICollection<CourseTag>? CourseTags { get; set; }
    public ICollection<JobTag>? JobTags { get; set; }
}
