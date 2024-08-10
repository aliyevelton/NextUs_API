using AutoMapper;
using Business.DTOs.CourseBookmarkDtos;
using Business.DTOs.CourseDtos;
using Business.DTOs.JobDtos;
using Business.Exceptions.CourseExceptions;
using Business.Services.Interfaces;
using Core.Entities;
using DataAccess.Repositories.Interfaces;

namespace Business.Services.Implementations;

public class CourseBookmarkService : ICourseBookmarkService
{
    private readonly IRepository<CourseBookmark> _repository;
    private readonly IRepository<Course> _courseRepository;
    private readonly IMapper _mapper;

    public CourseBookmarkService(IMapper mapper, IRepository<CourseBookmark> repository, IRepository<Course> courseRepository)
    {
        _mapper = mapper;
        _repository = repository;
        _courseRepository = courseRepository;
    }

    public async Task<CourseBookmark> GetByIdAsync(int id)
    {
        var courseBookmark = await _repository.GetSingleAsync(j => j.Id == id);

        return courseBookmark;
    }

    public async Task<List<CourseGetDto>> GetCourseBookmarksByUserIdAsync(string userId)
    {
        var courseBookmarks = await _repository.GetFilteredAsync(
             c => c.UserId == userId && !c.Course.IsDeleted,
             "Course.Company");

        var courseGetDtos = _mapper.Map<List<CourseGetDto>>(courseBookmarks);

        return courseGetDtos;
    }

    public Task<List<CourseBookmark>> GetCourseBookmarksAsync()
    {
        throw new NotImplementedException();
    }

    public async Task AddAsync(CourseBookmarkPostDto courseBookmarkPostDto)
    {
        var course = await _courseRepository.GetSingleAsync(c => c.Id == courseBookmarkPostDto.CourseId);

        if (course == null)
            throw new CourseNotFoundByIdException($"Course not found by id: {courseBookmarkPostDto.CourseId}");
        if (courseBookmarkPostDto.UserId == null)
            throw new ArgumentNullException("UserId", "UserId cannot be null");

        var isCourseBookmarked = await IsCourseBookmarkedByUserAsync(courseBookmarkPostDto.CourseId, courseBookmarkPostDto.UserId);

        if (isCourseBookmarked)
        {
            _repository.Delete(await _repository.GetSingleAsync(cb => cb.CourseId == courseBookmarkPostDto.CourseId && cb.UserId == courseBookmarkPostDto.UserId));
        } else
        {
            var courseBookmark = _mapper.Map<CourseBookmark>(courseBookmarkPostDto);
            await _repository.AddAsync(courseBookmark);
        }

        await _repository.SaveAsync();
    }

    public async Task<bool> IsCourseBookmarkedByUserAsync(int courseId, string userId)
    {
        return await _repository.IsExistsAsync(cb => cb.CourseId == courseId && cb.UserId == userId);
    }

    public async Task DeleteAsync(int id)
    {
        var courseBookmark = await _repository.GetSingleAsync(cb => cb.Id == id);

        if (courseBookmark == null)
            throw new ArgumentNullException("CourseBookmark", "CourseBookmark not found");

        _repository.Delete(courseBookmark);
        await _repository.SaveAsync();
    }
}
