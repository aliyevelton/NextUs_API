using Core.Entities.Common;

namespace Core.Entities;

public class JobCategory : BaseEntity
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public ICollection<Job>? Jobs { get; set; } 
}
