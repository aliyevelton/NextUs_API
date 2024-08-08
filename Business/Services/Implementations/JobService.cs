using AutoMapper;
using Business.DTOs.JobDtos;
using Business.Exceptions.JobExceptions;
using Business.Helpers.Enums;
using Business.Services.Interfaces;
using Core.Entities;
using DataAccess.Repositories.Interfaces;
using System.Linq;

namespace Business.Services.Implementations;

public class JobService : IJobService
{
    private readonly IRepository<Job> _repository;
    private readonly IRepository<JobCategory> _categoryRepository;
    private readonly IRepository<Company> _companyRepository;
    private readonly IRepository<Tag> _tagRepository;
    private readonly IJobBookmarkService _jobBookmarkService;
    private readonly IMapper _mapper;

    public JobService(IRepository<Job> repository, IMapper mapper, IRepository<JobCategory> catRepository, IRepository<Company> companyRepository, IRepository<Tag> tagRepository, IJobBookmarkService jobBookmarkService)
    {
        _repository = repository;
        _mapper = mapper;
        _categoryRepository = catRepository;
        _companyRepository = companyRepository;
        _tagRepository = tagRepository;
        _jobBookmarkService = jobBookmarkService;
    }

    public async Task<List<JobGetDto>> GetAllJobsAsync(string? title, string? location, int? jobType, int? categoryId, int? companyId, int? minSalary, bool? isFeatured, bool? isPremium, bool? isActive, int? skip, int? take)
    {
        var dbJobs = await _repository.GetFilteredAsync(j => (title == null || j.Title.Contains(title.ToLower())) &&
                                                          (location == null || j.Location.Contains(location.ToLower())) &&
                                                          (jobType == null || j.JobType == jobType) &&
                                                          (categoryId == null || j.CategoryId == categoryId) &&
                                                          (companyId == null || j.CompanyId == companyId) && (minSalary == null || j.ExactSalary >= minSalary || j.MinSalary >= minSalary) &&
                                                          (isFeatured == null || j.IsFeatured == isFeatured) &&
                                                          (isPremium == null || j.IsPremium == isPremium) &&
                                                          (isActive == null || j.IsActive == isActive) && !j.IsDeleted, "Company");

        if (skip != null && take != null)
            dbJobs = dbJobs.Skip(skip.Value).Take(take.Value).ToList();

        var jobs = _mapper.Map<List<JobGetDto>>(dbJobs);
        return jobs;
    }

    public async Task<JobDetailWithBookmarkDto> GetByIdAsync(int id, string? userId)
    {
        var job = await _repository.GetSingleAsync(j => j.Id == id && !j.IsDeleted, "Category", "Company");
        if (job == null)
            throw new JobNotFoundByIdException($"Job not found by id: {id}");

        bool isBookmarked = false;
        if (userId != null)
        {
            isBookmarked = await _jobBookmarkService.IsJobBookmarkedByUserAsync(id, userId);
        }
        
        var jobDetailDto = _mapper.Map<JobDetailDto>(job);

        var result = new JobDetailWithBookmarkDto
        {
            JobDetail = jobDetailDto,
            IsBookmarked = isBookmarked
        };

        return result;
    }


    public async Task AddAsync(JobPostDto jobPostDto)
    {
        bool isCategoryExists = await _categoryRepository.IsExistsAsync(c => c.Id == jobPostDto.CategoryId && !c.IsDeleted);
        bool isCompanyExists = await _companyRepository.IsExistsAsync(c => c.Id == jobPostDto.CompanyId && !c.IsDeleted);
        bool isJobTypeExists = Enum.IsDefined(typeof(JobTypes), (byte)jobPostDto.JobType);

        if (!isCategoryExists)
            throw new JobCategoryNotFoundException($"Category not found by id: {jobPostDto.CategoryId}");
        else if (!isCompanyExists)
            throw new JobCompanyNotFoundException($"Company not found by id: {jobPostDto.CompanyId}");
        else if (!isJobTypeExists)
            throw new JobTypeNotFoundException($"Job type not found by id: {jobPostDto.JobType}");
        else if (!Enum.IsDefined(typeof(SalaryType), jobPostDto.SalaryType)) 
            throw new SalaryTypeNotFoundException($"Salary type not found by id: {jobPostDto.SalaryType}");

        switch (jobPostDto.SalaryType)
        {
            case SalaryType.Exact:
                if (jobPostDto.ExactSalary == null || jobPostDto.ExactSalary == 0)
                    throw new SalaryException("Salary can not be null for exact salary");
                break;

            case SalaryType.Range:
                if (jobPostDto.MinSalary == null || jobPostDto.MaxSalary == null)
                    throw new SalaryException("Both minimum and maximum salary must be provided for salary range type.");
                if (jobPostDto.MinSalary > jobPostDto.MaxSalary)
                    throw new SalaryException("Minimum salary cannot be greater than maximum salary type.");
                break;

            case SalaryType.NotSpecified:
                // No additional validation needed
                break;

            default:
                throw new ArgumentOutOfRangeException();
        }

        var job = _mapper.Map<Job>(jobPostDto);

        if (jobPostDto.Tags != null && jobPostDto.Tags.Any())
        {
            job.Tags = new List<JobTag>();
            foreach (var tagName in jobPostDto.Tags)
            {
                var tag = await _tagRepository.GetSingleAsync(t => t.Name == tagName);
                if (tag == null)
                {
                    tag = new Tag { Name = tagName };
                    await _tagRepository.AddAsync(tag);
                    await _tagRepository.SaveAsync();
                }
                job.Tags.Add(new JobTag { Job = job, Tag = tag });
            }
        }

        await _repository.AddAsync(job);
        await _repository.SaveAsync();
    }
    public async Task UpdateAsync(int id, JobPutDto jobPutDto)
    {
        var job = await _repository.GetSingleAsync(j => j.Id == id && !j.IsDeleted);
        if (job == null)
            throw new JobNotFoundByIdException($"Job not found by id: {id}");

        bool isJobTypeExists = Enum.IsDefined(typeof(JobTypes), (byte)jobPutDto.JobType);
        bool isSalaryTypeExists = Enum.IsDefined(typeof(SalaryType), (byte)jobPutDto.SalaryType);

        if (!isJobTypeExists)
            throw new JobTypeNotFoundException($"Job type not found by id: {jobPutDto.JobType}");
        else if (!isSalaryTypeExists)
            throw new SalaryTypeNotFoundException($"Salary type not found by id: {jobPutDto.SalaryType}");

        switch (jobPutDto.SalaryType)
        {
            case SalaryType.Exact:
                if (jobPutDto.ExactSalary == null || jobPutDto.ExactSalary == 0)
                    throw new SalaryException("Salary can not be null for exact salary");
                break;

            case SalaryType.Range:
                if (jobPutDto.MinSalary == null || jobPutDto.MaxSalary == null)
                    throw new SalaryException("Both minimum and maximum salary must be provided for salary range type.");
                if (jobPutDto.MinSalary > jobPutDto.MaxSalary)
                    throw new SalaryException("Minimum salary cannot be greater than maximum salary type.");
                break;

            case SalaryType.NotSpecified:
                // No additional validation needed
                break;

            default:
                throw new ArgumentOutOfRangeException();
        }

        _mapper.Map(jobPutDto, job);

        if (jobPutDto.Tags != null && jobPutDto.Tags.Any())
        {
            job.Tags = new List<JobTag>();
            foreach (var tagName in jobPutDto.Tags)
            {
                var tag = await _tagRepository.GetSingleAsync(t => t.Name == tagName);
                if (tag == null)
                {
                    tag = new Tag { Name = tagName };
                    await _tagRepository.AddAsync(tag);
                    await _tagRepository.SaveAsync();
                }
                job.Tags.Add(new JobTag { Job = job, Tag = tag });
            }
        }

        _repository.Update(job);
        await _repository.SaveAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var job = await _repository.GetSingleAsync(j => j.Id == id && !j.IsDeleted);
        if (job == null)
            throw new JobNotFoundByIdException($"Job not found by id: {id}");

        job.IsDeleted = true;
        _repository.Update(job);
        await _repository.SaveAsync();
    }

    public async Task IncrementViewCountAsync(int id)
    {
        var job = await _repository.GetSingleAsync(j => j.Id == id && !j.IsDeleted);
        if (job == null)
            throw new JobNotFoundByIdException($"Job not found by id: {id}");

        job.Views++;
        await _repository.SaveAsync();
    }
}
