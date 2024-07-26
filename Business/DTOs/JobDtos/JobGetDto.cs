using Business.DTOs.CategoryDtos;
using Business.DTOs.CompanyDTOs;

namespace Business.DTOs.JobDtos;

public class JobGetDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Position { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Location { get; set; } = null!;
    public string JobType { get; set; } = null!;
    public int Salary { get; set; }
    public JobCategoryGetDto Category { get; set; } 
    public CompanyPostDto Company { get; set; }
    public int Views { get; set; }
    public bool IsFeatured { get; set; }
    public bool IsPremium { get; set; }
    public bool IsApproved { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime ExpireDate { get; set; }
    public virtual bool IsDeleted { get; set; }
}
