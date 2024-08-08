using AutoMapper;
using Business.DTOs.JobBookmarkDtos;
using Business.DTOs.JobDtos;
using Business.Exceptions.JobExceptions;
using Business.Services.Interfaces;
using Core.Entities;
using DataAccess.Repositories.Interfaces;

namespace Business.Services.Implementations;

public class JobBookmarkService : IJobBookmarkService
{
    private readonly IRepository<JobBookmark> _repository;
    private readonly IRepository<Job> _jobRepository;
    private readonly IMapper _mapper;

    public JobBookmarkService(IRepository<JobBookmark> jobBookmarkRepository, IMapper mapper, IRepository<Job> jobRepository)
    {
        _repository = jobBookmarkRepository;
        _mapper = mapper;
        _jobRepository = jobRepository;
    }

    public async Task<JobBookmark> GetByIdAsync(int id)
    {
        var jobBookmark = await _repository.GetSingleAsync(j => j.Id == id);

        return jobBookmark;
    }

    public Task<List<JobBookmark>> GetJobBookmarksAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<List<JobBookmarkGetDto>> GetJobBookmarksByUserIdAsync(string userId)
    {
        var jobBookmarks = await _repository.GetFilteredAsync(j => j.UserId == userId, "Job", "Job.Company");

        var jobBookmarkDtos = jobBookmarks.Select(jb => new JobBookmarkGetDto
        {
            Id = jb.Id,
            UserId = jb.UserId,
            Job = new JobDto
            {
                JobId = jb.Job.Id,
                Title = jb.Job.Title,
                CompanyName = jb.Job.Company.Name,
                CompanyLogo = jb.Job.Company.Logo
            }
        }).ToList();

        return jobBookmarkDtos;
    }

    public async Task AddAsync(JobBookmarkPostDto jobBookmarkPostDto)
    {
        var job = await _jobRepository.GetSingleAsync(j => j.Id == jobBookmarkPostDto.JobId);

        if (job == null)
            throw new JobNotFoundByIdException($"Job not found by id: {jobBookmarkPostDto.JobId}");
        if (jobBookmarkPostDto.UserId == null)
            throw new ArgumentNullException("UserId", "UserId cannot be null");

        var isJobBookmarked = await IsJobBookmarkedByUserAsync(jobBookmarkPostDto.JobId, jobBookmarkPostDto.UserId);
        if (isJobBookmarked)
        {
            _repository.Delete(await _repository.GetSingleAsync(jb => jb.JobId == jobBookmarkPostDto.JobId && jb.UserId == jobBookmarkPostDto.UserId));
        } else
        {
            var jobBookmark = _mapper.Map<JobBookmark>(jobBookmarkPostDto);
            await _repository.AddAsync(jobBookmark);
        }

        await _repository.SaveAsync();
    }

    public async Task<bool> IsJobBookmarkedByUserAsync(int jobId, string userId)
    {
        return await _repository.IsExistsAsync(jb => jb.JobId == jobId && jb.UserId == userId);
    }

    public async Task DeleteAsync(int id)
    {
        var jobBookmark = await _repository.GetSingleAsync(j => j.Id == id);

        if (jobBookmark == null)
            throw new ArgumentNullException("JobBookmark", "JobBookmark not found");

        _repository.Delete(jobBookmark);
        await _repository.SaveAsync();
    }
}
