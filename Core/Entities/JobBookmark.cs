using Core.Entities.Common;
using Core.Entities.Identity;

namespace Core.Entities;

public class JobBookmark : BaseEntity
{
    public int JobId { get; set; }
    public Job Job { get; set; }
    public string UserId { get; set; }
    public AppUser User { get; set; }
}
