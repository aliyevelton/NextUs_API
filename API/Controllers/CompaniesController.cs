using Business.DTOs.CompanyDTOs;
using Business.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CompaniesController : ControllerBase
{
    private readonly ICompanyService _companyService;

    public CompaniesController(ICompanyService companyService)
    {
        _companyService = companyService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCompaniesAsync(string? search)
    {
        return Ok(await _companyService.GetAllCompaniesAsync(search));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        return Ok(await _companyService.GetByIdAsync(id));
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromForm] CompanyPostDto company)
    {
        await _companyService.AddAsync(company);
        return StatusCode((int)HttpStatusCode.Created, "Company successfully created");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] CompanyPostDto company)
    {
        await _companyService.UpdateAsync(id, company);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await _companyService.DeleteAsync(id);
        return NoContent();
    }
}
