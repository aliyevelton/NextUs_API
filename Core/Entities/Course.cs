using Core.Entities.Common;

namespace Core.Entities;

public class Course : BaseEntity
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal? Price { get; set; }
    public string? Picture { get; set; } // not used yet
    public int CategoryId { get; set; }
    public CourseCategory Category { get; set; }
    public int CompanyId { get; set; }
    public Company Company { get; set; }
    public int TotalHours { get; set; }
    public string? Syllabus { get; set; }
    public string Location { get; set; } = null!;
    public bool IsApproved { get; set; }
    public bool IsActive { get; set; }
    public int CourseType { get; set; } // 1: Offline, 2: Online, 3: Hybrid
    public ICollection<CourseBookmark>? Bookmarks { get; set; }
    public ICollection<CourseTag>? Tags { get; set; }
}
