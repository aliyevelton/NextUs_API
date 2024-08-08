using Business.DTOs.CourseBookmarkDtos;
using Core.Entities;

namespace Business.Services.Interfaces;

public interface ICourseBookmarkService
{
    Task<CourseBookmark> GetByIdAsync(int id);
    Task<List<CourseBookmark>> GetCourseBookmarksAsync();
    Task<List<CourseBookmarkGetDto>> GetCourseBookmarksByUserIdAsync(string userId);
    Task AddAsync(CourseBookmarkPostDto CourseBookmarkPostDto);
    Task DeleteAsync(int id);
    Task<bool> IsCourseBookmarkedByUserAsync(int courseId, string userId);
}
