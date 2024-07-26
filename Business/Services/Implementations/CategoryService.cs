using AutoMapper;
using Business.DTOs.CategoryDtos;
using Business.Exceptions.CategoryExceptions;
using Business.Services.Interfaces;
using Core.Entities;
using DataAccess.Repositories.Interfaces;

namespace Business.Services.Implementations;

public class CategoryService : ICategoryService
{
    private readonly IRepository<JobCategory> _repository;
    private readonly IMapper _mapper;

    public CategoryService(IRepository<JobCategory> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<JobCategoryGetDto>> GetAllCategoriesAsync(string? search)
    {
        var jobCategories = await _repository.GetFilteredAsync(c => (search == null || c.Name.ToLower().Contains(search.ToLower()) && !c.IsDeleted));
        
        var jobCategoriesDto = _mapper.Map<List<JobCategoryGetDto>>(jobCategories);
        return jobCategoriesDto;
    }

    public async Task<JobCategoryGetDto> GetByIdAsync(int id)
    {
        var category = await _repository.GetSingleAsync(c => c.Id == id && !c.IsDeleted);
        if (category == null)
            throw new CategoryNotFoundByIdException($"Category not found by id: {id}");

        var categoryDto = _mapper.Map<JobCategoryGetDto>(category);
        return categoryDto;
    }

    public async Task AddAsync(JobCategoryPostDto category)
    {
        bool isExists = await _repository.IsExistsAsync(c => c.Name.ToLower() == category.Name.ToLower() && !c.IsDeleted);
        if(isExists)
            throw new CategoryAlreadyExistException($"Category already exists with the name: {category.Name}");

        var newCategory = _mapper.Map<JobCategory>(category);
        await _repository.AddAsync(newCategory);
        await _repository.SaveAsync();
    }

    public async Task UpdateAsync(int id, JobCategoryPutDto category)
    {
        var jobCategory = await _repository.GetSingleAsync(c => c.Id == id && !c.IsDeleted);
        if (jobCategory == null)
            throw new CategoryNotFoundByIdException($"Category not found by id: {id}");

        jobCategory.Name = category.Name;
        jobCategory.Description = category.Description;

        _repository.Update(jobCategory);
        await _repository.SaveAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var category = await _repository.GetSingleAsync(c => c.Id == id && c.IsDeleted);
        if (category == null)
            throw new CategoryNotFoundByIdException($"Category not found by id: {id}");

        category.IsDeleted = true;
        await _repository.SaveAsync();
    }
}
