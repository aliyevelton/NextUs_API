using Core.Entities.Common;

namespace Core.Entities;

public class Job : BaseEntity
{
    public string Title { get; set; } = null!;
    public string Position { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Location { get; set; } = null!;
    public decimal? ExactSalary { get; set; }
    public decimal? MinSalary { get; set; }
    public decimal? MaxSalary { get; set; }
    public byte SalaryType { get; set; }
    public int CategoryId { get; set; }
    public JobCategory Category { get; set; }
    public int CompanyId { get; set; }
    public Company Company { get; set; } = null!;
    public int JobType { get; set; } // 1: Full Time, 2: Part Time, 3: Remote, 4: Freelance, 5: Internship
    public int Views { get; set; }
    public bool IsFeatured { get; set; }
    public bool IsPremium { get; set; }
    public bool IsApproved { get; set; }
    public bool IsActive { get; set; }
    public DateTime ExpireDate { get; set; }
    public ICollection<JobApplication>? Applications { get; set; }
    public ICollection<JobBookmark>? Bookmarks { get; set; }
}
