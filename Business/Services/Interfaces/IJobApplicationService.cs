using Business.DTOs.JobApplicationDtos;
using Core.Entities;

namespace Business.Services.Interfaces;

public interface IJobApplicationService
{
    Task<JobApplicationGetDto> GetByIdAsync(int id);
    Task<List<JobApplicationGetDto>> GetJobApplicationsAsync();
    Task<List<JobApplicationGetDto>> GetJobApplicationsByUserIdAsync(string userId);
    Task<List<JobApplication>> GetJobApplicationsByJobIdAsync(int jobId);
    Task AddAsync(JobApplicationPostDto jobApplicationPostDto);
    Task DeleteAsync(int id);
}
