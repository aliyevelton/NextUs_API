using Business.DTOs.JobBookmarkDtos;
using Core.Entities;

namespace Business.Services.Interfaces;

public interface IJobBookmarkService
{
    Task<JobBookmark> GetByIdAsync(int id);
    Task<List<JobBookmark>> GetJobBookmarksAsync();
    Task<List<JobBookMarkGetDto>> GetJobBookmarksByUserIdAsync(string userId);
    Task AddAsync(JobBookmarkPostDto jobBookmarkPostDto);
    Task DeleteAsync(int id);
}
