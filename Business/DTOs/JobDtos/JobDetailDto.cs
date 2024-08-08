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
    public decimal? ExactSalary { get; set; }
    public decimal? MinSalary { get; set; }
    public decimal? MaxSalary { get; set; }
    public byte SalaryType { get; set; }
    public bool IsFeatured { get; set; }
    public bool IsPremium { get; set; }
    public int Views { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime ExpireDate { get; set; }
    public List<string>? Tags { get; set; }
}
