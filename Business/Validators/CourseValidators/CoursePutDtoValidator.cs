using Business.DTOs.CourseDtos;
using FluentValidation;

namespace Business.Validators.CourseValidators;

public class CoursePutDtoValidator : AbstractValidator<CoursePutDto>
{
    public CoursePutDtoValidator()
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
            .NotNull().WithMessage("Price is required")
            .NotEmpty()
            .GreaterThan(0);
        RuleFor(p => p.TotalHours)
            .NotNull().WithMessage("Total hours is required")
            .NotEmpty()
            .GreaterThan(0);
        RuleFor(p => p.Location)
            .NotNull().WithMessage("Location is required")
            .NotEmpty()
            .MaximumLength(30);
        RuleFor(p => p.CourseType)
            .GreaterThanOrEqualTo(1)
            .LessThanOrEqualTo(3);
    }
}
