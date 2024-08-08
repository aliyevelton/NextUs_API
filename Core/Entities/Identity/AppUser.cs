using Microsoft.AspNetCore.Identity;

namespace Core.Entities.Identity;

public class AppUser : IdentityUser
{
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string? ProfilePhoto { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }
    public bool IsActive { get; set; }
    public ICollection<JobApplication>? Applications { get; set; }
    public ICollection<JobBookmark>? Bookmarks { get; set; }
    public ICollection<CourseBookmark>? CourseBookmarks { get; set; }
}
