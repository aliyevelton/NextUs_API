using Business.DTOs.JobDtos;

namespace Business.Services.Interfaces;

public interface IJobService
{
    Task<List<JobGetDto>> GetAllJobsAsync(string? title, string? location, int? jobType, int? categoryId, int? companyId, int? salary, bool? isFeatured, bool? isPremium, bool? isActive, int? skip, int? take);
    Task<JobDetailWithBookmarkDto> GetByIdAsync(int id, string? userId);
    Task AddAsync(JobPostDto jobPostDto);
    Task UpdateAsync(int id, JobPutDto jobPutDto);
    Task DeleteAsync(int id);
    Task IncrementViewCountAsync(int id);
}
