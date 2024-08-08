using Business.DTOs.JobDtos;
using Business.Exceptions.JobExceptions;
using Business.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class JobsController : ControllerBase
{
    private readonly IJobService _jobService;

    public JobsController(IJobService jobService)
    {
        _jobService = jobService;
    }
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? title, [FromQuery] string? location, [FromQuery] int? jobType, [FromQuery] int? categoryId, [FromQuery] int? companyId, [FromQuery] int? minSalary, [FromQuery] bool? isFeatured, [FromQuery] bool? isPremium, [FromQuery] bool? isActive, [FromQuery] int? skip, [FromQuery] int? take)
    {
        var jobs = await _jobService.GetAllJobsAsync(title, location, jobType, categoryId, companyId, minSalary, isFeatured, isPremium, isActive, skip, take);
        return Ok(jobs);
    }
    [HttpPost]
    public async Task<IActionResult> Create(JobPostDto jobCreateDto)
    {
        await _jobService.AddAsync(jobCreateDto);
        return StatusCode(((int)HttpStatusCode.Created), "Job successfully created");
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var jobDetailWithBookmark = await _jobService.GetByIdAsync(id, userId);
        return Ok(jobDetailWithBookmark);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, JobPutDto jobPutDto)
    {
        await _jobService.UpdateAsync(id, jobPutDto);
        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        await _jobService.DeleteAsync(id);
        return NoContent();
    }

    [HttpPost("{id}/increment-view-count")]
    public async Task<IActionResult> IncrementViewCount(int id)
    {
        await _jobService.IncrementViewCountAsync(id);
        return NoContent();
    }
}
