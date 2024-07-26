using Business.DTOs.CategoryDtos;
using Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
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
    public async Task<IActionResult> AddAsync([FromBody] JobCategoryPostDto category)
    {
        await _categoryService.AddAsync(category);
        return StatusCode((int)HttpStatusCode.Created, "Category successfully created");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] JobCategoryPutDto category)
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
