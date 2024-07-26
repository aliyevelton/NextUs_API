using Business.DTOs.JobDtos;

namespace Business.Services.Interfaces;

public interface IJobService
{
    Task<List<JobGetDto>> GetAllJobsAsync(string? title, string? location, string? jobType, int? categoryId, int? companyId, int? salary, bool? isFeatured, bool? isPremium, bool? isActive);
    Task<JobGetDto> GetByIdAsync(int id);
    Task AddAsync(JobPostDto jobPostDto);
    Task UpdateAsync(int id, JobPutDto jobPutDto);
    Task DeleteAsync(int id);
}
