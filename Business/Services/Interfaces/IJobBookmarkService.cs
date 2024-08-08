using Business.DTOs.JobBookmarkDtos;
using Core.Entities;

namespace Business.Services.Interfaces;

public interface IJobBookmarkService
{
    Task<JobBookmark> GetByIdAsync(int id);
    Task<List<JobBookmark>> GetJobBookmarksAsync();
    Task<List<JobBookmarkGetDto>> GetJobBookmarksByUserIdAsync(string userId);
    Task AddAsync(JobBookmarkPostDto jobBookmarkPostDto);
    Task DeleteAsync(int id);
    Task<bool> IsJobBookmarkedByUserAsync(int jobId, string userId);
}
