using Core.Entities.Common;
using Core.Entities.Identity;

namespace Core.Entities;

public class JobApplication : BaseEntity
{
    public string FullName { get; set; } = null!;
    public string? CoverLetter { get; set; }
    public string Cv { get; set; } = null!;
    public int JobId { get; set; }
    public Job Job { get; set; }
    public string UserId { get; set; }
    public AppUser User { get; set; }
}
