using Business.DTOs.CourseCategoryDtos;

namespace Business.Services.Interfaces;

public interface ICourseCategoryService
{
    Task<List<CourseCategoryGetDto>> GetAllCategoriesAsync(string? search);
    Task<CourseCategoryGetDto> GetByIdAsync(int id);
    Task AddAsync(CourseCategoryPostDto category);
    Task UpdateAsync(int id, CourseCategoryPutDto category);
    Task DeleteAsync(int id);
}
