using Business.DTOs.JobApplicationDtos;
using Core.Entities;

namespace Business.Services.Interfaces;

public interface IJobApplicationService
{
    Task<JobApplication> GetByIdAsync(int id);
    Task<List<JobApplication>> GetJobApplicationsAsync();
    Task<List<JobApplication>> GetJobApplicationsByUserIdAsync(string userId);
    Task<List<JobApplication>> GetJobApplicationsByJobIdAsync(int jobId);
    Task AddAsync(JobApplicationPostDto jobApplicationPostDto);
    Task DeleteAsync(int id);
}
