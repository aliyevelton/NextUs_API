using AutoMapper;
using Business.DTOs.JobDtos;
using Business.Exceptions.JobExceptions;
using Business.Services.Interfaces;
using Core.Entities;
using DataAccess.Repositories.Interfaces;

namespace Business.Services.Implementations;

public class JobService : IJobService
{
    private readonly IRepository<Job> _repository;
    private readonly IMapper _mapper;

    public JobService(IRepository<Job> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<JobGetDto>> GetAllJobsAsync(string? title, string? location, string? jobType, int? categoryId, int? companyId, int? salary, bool? isFeatured, bool? isPremium, bool? isActive)
    {
        var dbJobs = await _repository.GetFilteredAsync(j => (title == null || j.Title.Contains(title.ToLower())) &&
                                                          (location == null || j.Location.Contains(location.ToLower())) &&
                                                          (jobType == null || j.JobType.Contains(jobType.ToLower())) &&
                                                          (categoryId == null || j.CategoryId == categoryId) &&
                                                          (companyId == null || j.CompanyId == companyId) &&
                                                          (salary == null || j.Salary == salary) &&
                                                          (isFeatured == null || j.IsFeatured == isFeatured) &&
                                                          (isPremium == null || j.IsPremium == isPremium) &&
                                                          (isActive == null || j.IsActive == isActive) && !j.IsDeleted, "Category", "Company");
        
        var jobs = _mapper.Map<List<JobGetDto>>(dbJobs);
        return jobs;
    }

    public async Task<JobGetDto> GetByIdAsync(int id)
    {
        var job = await _repository.GetSingleAsync(j => j.Id == id && !j.IsDeleted);
        if (job == null)
            throw new JobNotFoundByIdException($"Job not found by id: {id}");
        
        var jobGetDto = _mapper.Map<JobGetDto>(job);

        return jobGetDto;
    }


    public async Task AddAsync(JobPostDto jobPostDto)
    {
        bool isCategoryExists = await _repository.IsExistsAsync(j => j.CategoryId == jobPostDto.CategoryId && !j.IsDeleted);
        bool isCompanyExists = await _repository.IsExistsAsync(j => j.CompanyId == jobPostDto.CompanyId && !j.IsDeleted);
        if (!isCategoryExists)
            throw new JobCategoryNotFoundException($"Category not found by id: {jobPostDto.CategoryId}");
        else if (!isCompanyExists)
            throw new JobCompanyNotFoundException($"Company not found by id: {jobPostDto.CompanyId}");

        var job = _mapper.Map<Job>(jobPostDto);

        await _repository.AddAsync(job);
        await _repository.SaveAsync();
    }
    public async Task UpdateAsync(int id, JobPutDto jobPutDto)
    {
        var job = await _repository.GetSingleAsync(j => j.Id == id && !j.IsDeleted);
        if (job == null)
            throw new JobNotFoundByIdException($"Job not found by id: {id}");

        _mapper.Map(jobPutDto, job);

        _repository.Update(job);
        await _repository.SaveAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var job = await _repository.GetSingleAsync(j => j.Id == id && !j.IsDeleted);
        if (job == null)
            throw new JobNotFoundByIdException($"Job not found by id: {id}");

        job.IsDeleted = true;
        await _repository.SaveAsync();
    }
}
