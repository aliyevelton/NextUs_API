using Business.DTOs.CourseDtos;
using Business.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CoursesController : ControllerBase
{
    private readonly ICourseService _courseService;

    public CoursesController(ICourseService courseService)
    {
        _courseService = courseService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? title, [FromQuery]  string? location, [FromQuery] int? categoryId, [FromQuery] int? companyId, [FromQuery] int? courseType, [FromQuery] int? minPrice, [FromQuery] bool? isApproved, [FromQuery] bool? isActive, [FromQuery] int? skip, [FromQuery] int? take)
    {
        var courses = await _courseService.GetAllCoursesAsync(title, location, categoryId, companyId, courseType, minPrice, isApproved, isActive, skip, take);
        return Ok(courses);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm]  CoursePostDto courseCreateDto)
    {
        await _courseService.AddAsync(courseCreateDto);
        return StatusCode(((int)HttpStatusCode.Created), "Course successfully created");
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var courseDetailWithBookmark = await _courseService.GetByIdAsync(id, userId);
        return Ok(courseDetailWithBookmark);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromForm] CoursePutDto coursePutDto)
    {
        await _courseService.UpdateAsync(id, coursePutDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _courseService.DeleteAsync(id);
        return NoContent();
    }
}
