using Business.DTOs.CourseCategoryDtos;
using Business.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CourseCategoriesController : ControllerBase
{
    private readonly ICourseCategoryService _categoryService;

    public CourseCategoriesController(ICourseCategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCategoriesAsync(string? search)
    {
        return Ok(await _categoryService.GetAllCategoriesAsync(search));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        return Ok(await _categoryService.GetByIdAsync(id));
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] CourseCategoryPostDto category)
    {
        await _categoryService.AddAsync(category);
        return StatusCode(StatusCodes.Status201Created, "Category successfully created");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] CourseCategoryPutDto category)
    {
        await _categoryService.UpdateAsync(id, category);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await _categoryService.DeleteAsync(id);
        return NoContent();
    }
}
