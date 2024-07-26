using Business.DTOs.CategoryDtos;

namespace Business.Services.Interfaces;

public interface ICategoryService
{
    Task<List<JobCategoryGetDto>> GetAllCategoriesAsync(string? search);
    Task<JobCategoryGetDto> GetByIdAsync(int id);
    Task AddAsync(JobCategoryPostDto categoryDto);
    Task UpdateAsync(int id, JobCategoryPutDto categoryDto);
    Task DeleteAsync(int id);
}
