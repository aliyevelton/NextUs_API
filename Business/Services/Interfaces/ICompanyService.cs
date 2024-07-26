using Business.DTOs.CompanyDtos;
using Business.DTOs.CompanyDTOs;

namespace Business.Services.Interfaces;

public interface ICompanyService
{
    Task<List<CompanyGetDto>> GetAllCompaniesAsync(string? search);
    Task<CompanyGetDto> GetByIdAsync(int id);
    Task AddAsync(CompanyPostDto companyDto);
    Task UpdateAsync(int id, CompanyPostDto companyDto);
    Task DeleteAsync(int id);
}
