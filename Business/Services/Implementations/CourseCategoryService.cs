using AutoMapper;
using Business.DTOs.CourseCategoryDtos;
using Business.Exceptions.CategoryExceptions;
using Business.Services.Interfaces;
using Core.Entities;
using DataAccess.Repositories.Interfaces;

namespace Business.Services.Implementations;

public class CourseCategoryService : ICourseCategoryService
{
    private readonly IRepository<CourseCategory> _repository;
    private readonly IMapper _mapper;

    public CourseCategoryService(IRepository<CourseCategory> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<CourseCategoryGetDto>> GetAllCategoriesAsync(string? search)
    {
        var courseCategories = await _repository.GetFilteredAsync(c => (search == null || c.Name.ToLower().Contains(search.ToLower()) && !c.IsDeleted));
        
        var courseCategoriesDto = _mapper.Map<List<CourseCategoryGetDto>>(courseCategories);
        return courseCategoriesDto;
    }

    public async Task<CourseCategoryGetDto> GetByIdAsync(int id)
    {
        var category = await _repository.GetSingleAsync(c => c.Id == id && !c.IsDeleted);
        if (category == null)
            throw new CategoryNotFoundByIdException($"Category not found by id: {id}");

        var categoryDto = _mapper.Map<CourseCategoryGetDto>(category);
        return categoryDto;
    }

    public async Task AddAsync(CourseCategoryPostDto category)
    {
        bool isExists = await _repository.IsExistsAsync(c => c.Name.ToLower() == category.Name.ToLower() && !c.IsDeleted);
        if(isExists)
            throw new CategoryAlreadyExistException($"Category already exists with the name: {category.Name}");

        var newCategory = _mapper.Map<CourseCategory>(category);
        await _repository.AddAsync(newCategory);
        await _repository.SaveAsync();
    }

    public async Task UpdateAsync(int id, CourseCategoryPutDto category)
    {
        var courseCategory = await _repository.GetSingleAsync(c => c.Id == id && !c.IsDeleted);
        if (courseCategory == null)
            throw new CategoryNotFoundByIdException($"Category not found by id: {id}");

        var newCategory = _mapper.Map(category, courseCategory);

        _repository.Update(courseCategory);
        await _repository.SaveAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var category = await _repository.GetSingleAsync(c => c.Id == id && c.IsDeleted);
        if (category == null)
            throw new CategoryNotFoundByIdException($"Category not found by id: {id}");

        _repository.Delete(category);
        await _repository.SaveAsync();
    }
}
