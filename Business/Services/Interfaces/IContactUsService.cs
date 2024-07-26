using Business.DTOs.ContactUsDtos;

namespace Business.Services.Interfaces;

public interface IContactUsService
{
    Task AddAsync(ContactUsDto contactUsDto);
    Task<List<ContactUsDto>> GetAllAsync();
    Task<ContactUsDto> GetByIdAsync(int id);
    Task DeleteAsync(int id);
}
