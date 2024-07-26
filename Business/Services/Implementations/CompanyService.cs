using AutoMapper;
using Business.DTOs.CompanyDtos;
using Business.DTOs.CompanyDTOs;
using Business.Exceptions.CompanyExceptions;
using Business.HelperServices.Interfaces;
using Business.Services.Interfaces;
using Core.Entities;
using DataAccess.Repositories.Interfaces;

namespace Business.Services.Implementations;

public class CompanyService : ICompanyService
{
    private readonly IRepository<Company> _repository;
    private readonly IMapper _mapper;
    private readonly IFileService _fileService;

    public CompanyService(IRepository<Company> repository, IMapper mapper, IFileService fileService)
    {
        _repository = repository;
        _mapper = mapper;
        _fileService = fileService;
    }

    public async Task<List<CompanyGetDto>> GetAllCompaniesAsync(string? search)
    {
        var companies = await _repository.GetFilteredAsync(c => (search == null || c.Name.ToLower().Contains(search.ToLower()) && !c.IsDeleted));

        if (companies.Count == 0)
            throw new CompanyNotFoundException("No companies found");

        var companiesDto = _mapper.Map<List<CompanyGetDto>>(companies);
        return companiesDto;
    }

    public async Task<CompanyGetDto> GetByIdAsync(int id)
    {
        var company = await _repository.GetSingleAsync(c => c.Id == id && !c.IsDeleted);
        if (company == null)
            throw new CompanyNotFoundByIdException($"Company not found by id: {id}");

        var companyDto = _mapper.Map<CompanyGetDto>(company);
        return companyDto;
    }

    public async Task AddAsync(CompanyPostDto companyPostDto)
    {
        bool isExists = await _repository.IsExistsAsync(c => c.Name.ToLower() == companyPostDto.Name && !c.IsDeleted);
        if (isExists)
            throw new CompanyAlreadyExistException($"Company already exists with the name: {companyPostDto.Name}");

        var newCompany = _mapper.Map<Company>(companyPostDto);

        if (companyPostDto.Logo != null)
        {
            string fileName = await _fileService.UploadFileAsync(companyPostDto.Logo, "image/", 3000, "companies");

            newCompany.Logo = fileName;
        }

        if (companyPostDto.CoverImage != null)
        {
            string fileName = await _fileService.UploadFileAsync(companyPostDto.CoverImage, "image/", 3000, "companies");

            newCompany.CoverImage = fileName;
        }

        await _repository.AddAsync(newCompany);
        await _repository.SaveAsync();
    }

    public async Task UpdateAsync(int id, CompanyPostDto companyPostDto)
    {
        var company = await _repository.GetSingleAsync(c => c.Id == id && !c.IsDeleted);
        if (company == null)
            throw new CompanyNotFoundByIdException($"Company not found by id: {id}");

        _mapper.Map(companyPostDto, company);
        _repository.Update(company);
        await _repository.SaveAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var company = await _repository.GetSingleAsync(c => c.Id == id && !c.IsDeleted);
        if (company == null)
            throw new CompanyNotFoundByIdException($"Company not found by id: {id}");

        company.IsDeleted = true;
        await _repository.SaveAsync();
    }
}
