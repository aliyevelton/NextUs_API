using AutoMapper;
using Business.DTOs.JobBookmarkDtos;
using Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class JobBookmarksController : ControllerBase
{
    private readonly IJobBookmarkService _jobBookmarkService;
    private readonly IMapper _mapper;

    public JobBookmarksController(IJobBookmarkService jobBookmarkService, IMapper mapper)
    {
        _jobBookmarkService = jobBookmarkService;
        _mapper = mapper;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var jobBookmark = await _jobBookmarkService.GetByIdAsync(id);

        if (jobBookmark == null)
            return NotFound();

        return Ok(jobBookmark);
    }

    [HttpGet]
    public async Task<IActionResult> GetJobBookmarksByUserIdAsync()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; // Get user ID from claims
        if (userId == null)
            return Unauthorized();

        var jobBookmarks = await _jobBookmarkService.GetJobBookmarksByUserIdAsync(userId);

        if (jobBookmarks == null)
            return NotFound();

        return Ok(jobBookmarks);
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync(JobBookmarkPostDto jobBookmarkPostDto)
    {
        await _jobBookmarkService.AddAsync(jobBookmarkPostDto);

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await _jobBookmarkService.DeleteAsync(id);

        return Ok();
    }
}
