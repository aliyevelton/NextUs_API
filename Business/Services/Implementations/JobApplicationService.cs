using AutoMapper;
using Business.DTOs.JobApplicationDtos;
using Business.Exceptions.JobApplicationExceptions;
using Business.Exceptions.JobExceptions;
using Business.HelperServices.Interfaces;
using Business.Services.Interfaces;
using Core.Entities;
using DataAccess.Repositories.Interfaces;

namespace Business.Services.Implementations;

public class JobApplicationService : IJobApplicationService
{
    private readonly IRepository<JobApplication> _repository;
    private readonly IRepository<Job> _jobRepository;
    private readonly IFileService _fileService;
    private readonly IMapper _mapper;

    public JobApplicationService(IRepository<JobApplication> jobApplicationRepository, IRepository<Job> jobRepository, IFileService fileService, IMapper mapper)
    {
        _repository = jobApplicationRepository;
        _jobRepository = jobRepository;
        _fileService = fileService;
        _mapper = mapper;
    }

    public async Task<JobApplicationGetDto> GetByIdAsync(int id)
    {
        var jobApplication = await _repository.GetSingleAsync(j => j.Id == id && !j.IsDeleted, "Job", "Job.Company");

        if (jobApplication == null)
            throw new JobApplicationNotFoundException($"Job application not found by id: {id}");

        var jobApplicationDto = _mapper.Map<JobApplicationGetDto>(jobApplication);

        return jobApplicationDto;
    }

    public async Task<List<JobApplicationGetDto>> GetJobApplicationsByUserIdAsync(string userId)
    {
        var jobApplications = await _repository.GetFilteredAsync(
            j => j.UserId == userId && !j.IsDeleted,  
            "Job", "Job.Company"                      
        );

        if (jobApplications == null || !jobApplications.Any())
            throw new JobApplicationNotFoundException("No job applications found for the specified user.");

        var jobApplicationDtos = _mapper.Map<List<JobApplicationGetDto>>(jobApplications);

        return jobApplicationDtos;
    }


    public async Task<List<JobApplicationGetDto>> GetJobApplicationsAsync()
    {
        var jobApplications = await _repository.GetFilteredAsync(
            j => !j.IsDeleted,  
            "Job", "Job.Company"
        );

        if (jobApplications == null || !jobApplications.Any())
            throw new JobApplicationNotFoundException("No job applications found.");

        var jobApplicationDtos = _mapper.Map<List<JobApplicationGetDto>>(jobApplications);

        return jobApplicationDtos;
    }


    public async Task<List<JobApplication>> GetJobApplicationsByJobIdAsync(int jobId)
    {
        var jobApplications = await _repository.GetFilteredAsync(j => j.JobId == jobId);

        if (jobApplications == null)
            throw new JobApplicationNotFoundException("No job applications found");

        return jobApplications;
    }

    public async Task AddAsync(JobApplicationPostDto jobApplicationPostDto)
    {
        bool isJobExists = await _jobRepository.IsExistsAsync(j => j.Id == jobApplicationPostDto.JobId && !j.IsDeleted);

        if (!isJobExists)
            throw new JobNotFoundByIdException($"Job not found by id: {jobApplicationPostDto.JobId}");

        var jobApplication = _mapper.Map<JobApplication>(jobApplicationPostDto);

        if (jobApplicationPostDto.Cv != null)
        {
            string fileName = await _fileService.UploadFileAsync(jobApplicationPostDto.Cv, "application/", 5000, "files", "jobApplications");

            jobApplication.Cv = fileName;
        }

        await _repository.AddAsync(jobApplication);
        await _repository.SaveAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var jobApplication = await _repository.GetSingleAsync(j => j.Id == id && !j.IsDeleted);

        if (jobApplication == null)
            throw new JobApplicationNotFoundException($"Job application not found by id: {id}");

        jobApplication.IsDeleted = true;
        await _repository.SaveAsync();
    }
}
