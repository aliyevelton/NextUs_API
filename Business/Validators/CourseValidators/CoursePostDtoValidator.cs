using Business.DTOs.CourseDtos;
using FluentValidation;

namespace Business.Validators.CourseValidators;

public class CoursePostDtoValidator : AbstractValidator<CoursePostDto>
{
    public CoursePostDtoValidator()
    {
        RuleFor(p => p.Title)
            .NotNull().WithMessage("Title is required")
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(75);
        RuleFor(p => p.Description)
            .NotNull().WithMessage("Description is required")
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(5000);
        RuleFor(p => p.Price)
            .GreaterThanOrEqualTo(0);
        RuleFor(p => p.CategoryId)
            .NotNull().WithMessage("Category is required")
            .NotEmpty();
        RuleFor(p => p.CompanyId)
            .NotNull().WithMessage("Company is required")
            .NotEmpty();
        RuleFor(p => p.TotalHours)
            .GreaterThanOrEqualTo(0);
        RuleFor(p => p.Location)
            .NotNull().WithMessage("Location is required")
            .NotEmpty()
            .MaximumLength(30);
        RuleFor(p => p.CourseType)
            .GreaterThanOrEqualTo(1)
            .LessThanOrEqualTo(3);
    }
}
