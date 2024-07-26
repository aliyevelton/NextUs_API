using Core.Entities.Common;

namespace Core.Entities;

public class JobTag : BaseEntity
{
    public ICollection<Job> Jobs { get; set; } 
    public ICollection<JTag> Tags { get; set; } 
}
