using Core.Entities.Common;

namespace Core.Entities;

public class Job : BaseEntity
{
    public string Title { get; set; } = null!;
    public string Position { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Location { get; set; } = null!;
    public string JobType { get; set; } = null!;
    public int Salary { get; set; }
    public int CategoryId { get; set; }
    public JobCategory Category { get; set; } = null!;
    public int CompanyId { get; set; }
    public Company Company { get; set; } = null!;
    public int Views { get; set; }
    public bool IsFeatured { get; set; }
    public bool IsPremium { get; set; }
    public bool IsApproved { get; set; }
    public bool IsActive { get; set; }
    public DateTime ExpireDate { get; set; }
}
