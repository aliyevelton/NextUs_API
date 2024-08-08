using Core.Entities.Common;
using Core.Entities.Identity;

namespace Core.Entities;

public class CourseBookmark : BaseEntity
{
    public int CourseId { get; set; }
    public Course Course { get; set; } 
    public string UserId { get; set; }
    public AppUser User { get; set; } 
}
