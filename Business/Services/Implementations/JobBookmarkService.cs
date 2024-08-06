using AutoMapper;
using Business.DTOs.JobBookmarkDtos;
using Business.DTOs.JobDtos;
using Business.Services.Interfaces;
using Core.Entities;
using DataAccess.Repositories.Interfaces;

namespace Business.Services.Implementations;

public class JobBookmarkService : IJobBookmarkService
{
    private readonly IRepository<JobBookmark> _repository;
    private readonly IMapper _mapper;

    public JobBookmarkService(IRepository<JobBookmark> jobBookmarkRepository, IMapper mapper)
    {
        _repository = jobBookmarkRepository;
        _mapper = mapper;
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

    public async Task<List<JobBookMarkGetDto>> GetJobBookmarksByUserIdAsync(string userId)
    {
        var jobBookmarks = await _repository.GetFilteredAsync(j => j.UserId == userId, "Job", "Job.Company");

        var jobBookmarkDtos = jobBookmarks.Select(jb => new JobBookMarkGetDto
        {
            Id = jb.Id,
            UserId = jb.UserId,
            Job = new JobDto
            {
                JobId = jb.Job.Id,
                Title = jb.Job.Title,
                CompanyName = jb.Job.Company.Name
            }
        }).ToList();

        return jobBookmarkDtos;
    }

    public async Task AddAsync(JobBookmarkPostDto jobBookmarkPostDto)
    {
        if (jobBookmarkPostDto.JobId <= 0)
            throw new ArgumentNullException("JobId", "JobId cannot be zero or lower");
        if (jobBookmarkPostDto.UserId == null)
            throw new ArgumentNullException("UserId", "UserId cannot be null");

        var jobBookmark = _mapper.Map<JobBookmark>(jobBookmarkPostDto);

        await _repository.AddAsync(jobBookmark);
        await _repository.SaveAsync();
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
