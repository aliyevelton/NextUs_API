using Business.DTOs.CourseBookmarkDtos;
using Business.DTOs.CourseDtos;
using Core.Entities;

namespace Business.Services.Interfaces;

public interface ICourseBookmarkService
{
    Task<CourseBookmark> GetByIdAsync(int id);
    Task<List<CourseBookmark>> GetCourseBookmarksAsync();
    Task<List<CourseGetDto>> GetCourseBookmarksByUserIdAsync(string userId);
    Task AddAsync(CourseBookmarkPostDto CourseBookmarkPostDto);
    Task DeleteAsync(int id);
    Task<bool> IsCourseBookmarkedByUserAsync(int courseId, string userId);
}
