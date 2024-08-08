using AutoMapper;
using Business.DTOs.CourseDtos;
using Business.Exceptions.CompanyExceptions;
using Business.Exceptions.CourseExceptions;
using Business.Helpers.Enums;
using Business.HelperServices.Interfaces;
using Business.Services.Interfaces;
using Core.Entities;
using DataAccess.Repositories.Interfaces;

namespace Business.Services.Implementations;

public class CourseService : ICourseService
{
    private readonly IRepository<Course> _repository;
    private readonly IRepository<CourseCategory> _categoryRepository;
    private readonly IRepository<Company> _companyRepository;
    private readonly IRepository<Tag> _tagRepository;
    private readonly IJobBookmarkService _jobBookmarkService;
    private readonly IFileService _fileService;
    private readonly IMapper _mapper;

    public CourseService(IRepository<Course> repository, IMapper mapper, IRepository<CourseCategory> categoryRepository, IRepository<Company> companyRepository, IRepository<Tag> tagRepository, IFileService fileService, IJobBookmarkService jobBookmarkService)
    {
        _repository = repository;
        _mapper = mapper;
        _categoryRepository = categoryRepository;
        _companyRepository = companyRepository;
        _tagRepository = tagRepository;
        _fileService = fileService;
        _jobBookmarkService = jobBookmarkService;
    }

    public async Task<List<CourseGetDto>> GetAllCoursesAsync(string? title, string? location, int? categoryId, int? companyId, int? courseType, int? minPrice, bool? isActive, bool? isApproved, int? skip, int? take)
    {
        var dbCourses = await _repository.GetFilteredAsync(c => (title == null || c.Title.Contains(title.ToLower())) &&
            (location == null || c.Location.Contains(location.ToLower())) && (categoryId == null || c.CategoryId == categoryId) && (companyId == null || c.CompanyId == companyId) && (minPrice == null || c.Price >= minPrice) && (isActive == null || c.IsActive == isActive) && (isApproved == null || c.IsApproved == isApproved) && (courseType == null || c.CourseType == courseType) && !c.IsDeleted, "Company");

        if (skip != null && take != null)
            dbCourses = dbCourses.Skip(skip.Value).Take(take.Value).ToList();

        var courses = _mapper.Map<List<CourseGetDto>>(dbCourses);
        return courses;
    }

    public async Task<CourseDetailWithBookmarkDto> GetByIdAsync(int id, string? userId)
    {
        var course = await _repository.GetSingleAsync(c => c.Id == id && !c.IsDeleted, "Category", "Company");
        if (course == null)
            throw new CourseNotFoundByIdException($"Course not found by id: {id}");

        bool isBookmarked = false;
        if (userId != null)
        {
            isBookmarked = await _jobBookmarkService.IsJobBookmarkedByUserAsync(id, userId);
        }

        var courseDetailDto = _mapper.Map<CourseDetailDto>(course);

        var result = new CourseDetailWithBookmarkDto
        {
            CourseDetail = courseDetailDto,
            IsBookmarked = isBookmarked
        };

        return result;
    }

    public async Task AddAsync(CoursePostDto coursePostDto)
    {
        bool isCategoryExists = await _categoryRepository.IsExistsAsync(c => c.Id == coursePostDto.CategoryId && !c.IsDeleted);
        bool isCompanyExists = await _companyRepository.IsExistsAsync(c => c.Id == coursePostDto.CompanyId && !c.IsDeleted);
        bool isCourseTypeExists = Enum.IsDefined(typeof(CourseTypes), (byte)coursePostDto.CourseType);

        if (!isCategoryExists)
            throw new CourseCategoryNotFoundByIdException($"Course category not found by id: {coursePostDto.CategoryId}");
        if (!isCompanyExists)
            throw new CompanyNotFoundByIdException($"Company not found by id: {coursePostDto.CompanyId}");
        if (!isCourseTypeExists)
            throw new CourseTypeNotFoundException($"Course type not found by id: {coursePostDto.CourseType}");

        var course = _mapper.Map<Course>(coursePostDto);

        if (coursePostDto.Syllabus != null)
        {
            string fileName = await _fileService.UploadFileAsync(coursePostDto.Syllabus, "application/", 5000, "files", "courseSyllabus");
            course.Syllabus = fileName;
        }

        if (coursePostDto.Tags != null && coursePostDto.Tags.Any())
        {
            course.Tags = new List<CourseTag>();
            foreach (var tagName in coursePostDto.Tags)
            {
                var tag = await _tagRepository.GetSingleAsync(t => t.Name == tagName);
                if (tag == null)
                {
                    tag = new Tag { Name = tagName };
                    await _tagRepository.AddAsync(tag);
                    await _tagRepository.SaveAsync();
                }

                course.Tags.Add(new CourseTag { Tag = tag });
            }
        }

        await _repository.AddAsync(course);
        await _repository.SaveAsync();
    }

    public async Task UpdateAsync(int id, CoursePutDto coursePutDto)
    {
        var course = await _repository.GetSingleAsync(c => c.Id == id && !c.IsDeleted);
        if (course == null)
            throw new CourseNotFoundByIdException($"Course not found by id: {id}");

        bool isCourseTypeExists = Enum.IsDefined(typeof(CourseTypes), (byte)coursePutDto.CourseType);

        if (!isCourseTypeExists)
            throw new CourseTypeNotFoundException($"Course type not found by id: {coursePutDto.CourseType}");

        _mapper.Map(coursePutDto, course);

        if (coursePutDto.Syllabus != null && course.Syllabus != null)
        {
            if (!coursePutDto.Syllabus.FileName.Contains(course.Syllabus))
                _fileService.DeleteCourseSyllabusAsync(course.Syllabus);

            string fileName = await _fileService.UploadFileAsync(coursePutDto.Syllabus, "application/", 5000, "files", "courseSyllabus");
        } else if (coursePutDto.Syllabus != null && course.Syllabus == null)
        {
            string fileName = await _fileService.UploadFileAsync(coursePutDto.Syllabus, "application/", 5000, "files", "courseSyllabus");

            course.Syllabus = fileName;
        } else if (coursePutDto.Syllabus == null && course.Syllabus != null)
        {
            _fileService.DeleteCourseSyllabusAsync(course.Syllabus);

            course.Syllabus = null;
        }

        if (coursePutDto.Tags != null && coursePutDto.Tags.Any())
        {
            course.Tags = new List<CourseTag>();
            foreach (var tagName in coursePutDto.Tags)
            {
                var tag = await _tagRepository.GetSingleAsync(t => t.Name == tagName);
                if (tag == null)
                {
                    tag = new Tag { Name = tagName };
                    await _tagRepository.AddAsync(tag);
                    await _tagRepository.SaveAsync();
                }

                course.Tags.Add(new CourseTag { Tag = tag });
            }
        }

        _repository.Update(course);
        await _repository.SaveAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var course = await _repository.GetSingleAsync(c => c.Id == id && !c.IsDeleted);
        if (course == null)
            throw new CourseNotFoundByIdException($"Course not found by id: {id}");

        course.IsDeleted = true;
        _repository.Update(course);
        await _repository.SaveAsync();
    }
}
