using Business.DTOs.ContactUsDtos;
using Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContactUsController : ControllerBase
{
    private readonly IContactUsService _contactUsService;

    public ContactUsController(IContactUsService contactUsService)
    {
        _contactUsService = contactUsService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        return Ok(await _contactUsService.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        return Ok(await _contactUsService.GetByIdAsync(id));
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] ContactUsDto contactUs)
    {
        await _contactUsService.AddAsync(contactUs);
        return StatusCode((int)HttpStatusCode.Created, "Successfully sent");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await _contactUsService.DeleteAsync(id);
        return NoContent();
    }
}
