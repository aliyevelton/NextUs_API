using Business.DTOs.JobApplicationDtos;
using Business.Services.Implementations;
using Business.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class JobApplicationsController : ControllerBase
{
    private readonly IJobApplicationService _jobApplicationService;

    public JobApplicationsController(IJobApplicationService jobApplicationService)
    {
        _jobApplicationService = jobApplicationService;
    }

    [HttpGet]
    public async Task<IActionResult> GetJobApplications()
    {
        var jobApplications = await _jobApplicationService.GetJobApplicationsAsync();

        return Ok(jobApplications);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        return Ok(await _jobApplicationService.GetByIdAsync(id));
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetJobApplicationsByUserId()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; // Get user ID from claims
        if (userId == null)
            return Unauthorized();

        var jobApplications = await _jobApplicationService.GetJobApplicationsByUserIdAsync(userId);



        return Ok(jobApplications);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] JobApplicationPostDto jobApplicationPostDto)
    {
        await _jobApplicationService.AddAsync(jobApplicationPostDto);

        return StatusCode(((int)HttpStatusCode.Created), "Application is successfully submitted");
    }
}
