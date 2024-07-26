using Business.DTOs.JobDtos;
using Business.Exceptions.JobExceptions;
using Business.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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
    public async Task<IActionResult> GetAll([FromQuery] string? title, [FromQuery] string? location, [FromQuery] string? jobType, [FromQuery] int? categoryId, [FromQuery] int? companyId, [FromQuery] int? salary, [FromQuery] bool? isFeatured, [FromQuery] bool? isPremium, [FromQuery] bool? isActive)
    {
        var jobs = await _jobService.GetAllJobsAsync(title, location, jobType, categoryId, companyId, salary, isFeatured, isPremium, isActive);
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
        return Ok(await _jobService.GetByIdAsync(id));
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
}
