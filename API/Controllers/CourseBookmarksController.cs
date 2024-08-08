using AutoMapper;
using Business.DTOs.CourseBookmarkDtos;
using Business.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CourseBookmarksController : ControllerBase
{
    private readonly ICourseBookmarkService _courseBookmarkService;

    public CourseBookmarksController(ICourseBookmarkService courseBookmarkService)
    {
        _courseBookmarkService = courseBookmarkService;
    }

    [HttpPost("bookmark")]
    public async Task<IActionResult> AddAsync(CourseBookmarkPostDto courseBookmarkPostDto)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; // Get user ID from claims
        if (userId == null)
            return Unauthorized();

        courseBookmarkPostDto.UserId = userId;

        await _courseBookmarkService.AddAsync(courseBookmarkPostDto);

        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var courseBookmark = await _courseBookmarkService.GetByIdAsync(id);

        if (courseBookmark == null)
            return NotFound();

        return Ok(courseBookmark);
    }

    [HttpGet("user-bookmarks")]
    public async Task<IActionResult> GetCourseBookmarksByUserIdAsync()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; // Get user ID from claims
        if (userId == null)
            return Unauthorized();

        var courseBookmarks = await _courseBookmarkService.GetCourseBookmarksByUserIdAsync(userId);

        if (courseBookmarks == null)
            return NotFound();

        return Ok(courseBookmarks);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await _courseBookmarkService.DeleteAsync(id);

        return Ok();
    }

    [HttpGet("is-bookmarked")]
    public async Task<IActionResult> IsCourseBookmarkedByUserAsync(int courseId)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
            return Unauthorized();

        var isBookmarked = await _courseBookmarkService.IsCourseBookmarkedByUserAsync(courseId, userId);

        return Ok(isBookmarked);
    }
}
