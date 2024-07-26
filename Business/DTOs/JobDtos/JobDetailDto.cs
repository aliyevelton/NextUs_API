using Business.DTOs.CategoryDtos;
using Business.DTOs.CompanyDTOs;

namespace Business.DTOs.JobDtos;

public class JobDetailDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
    public string JobType { get; set; }
    public JobCategoryGetDto Category { get; set; }
    public CompanyPostDto Company { get; set; }
    public int Salary { get; set; }
    public bool IsFeatured { get; set; }
    public bool IsPremium { get; set; }
    public int Views { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
