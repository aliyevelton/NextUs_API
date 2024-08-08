using Business.DTOs.CourseDtos;

namespace Business.Services.Interfaces;

public interface ICourseService
{
    Task<List<CourseGetDto>> GetAllCoursesAsync(string? title, string? location, int? categoryId, int? companyId,int? courseType, int? price, bool? isApproved, bool? isActive, int? skip, int? take);
    Task<CourseDetailWithBookmarkDto> GetByIdAsync(int id, string? userId);
    Task AddAsync(CoursePostDto coursePostDto);
    Task UpdateAsync(int id, CoursePutDto coursePutDto);
    Task DeleteAsync(int id);
}