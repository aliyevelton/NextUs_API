using Business.DTOs.CourseBookmarkDtos;
using FluentValidation;

namespace Business.Validators.CourseBookmarkValidators;

public class CourseBookmarkValidator : AbstractValidator<CourseBookmarkPostDto>
{
    public CourseBookmarkValidator()
    {
        RuleFor(p => p.CourseId)
            .NotNull()
            .WithMessage("Course id is required");
        RuleFor(p => p.UserId)
            .NotNull()
            .WithMessage("User id is required");
    }
}
