using Business.DTOs.CategoryDtos;
using FluentValidation;

namespace Business.Validators.JobCategoryValidators;

public class JobCategoryPostDtoValidator : AbstractValidator<JobCategoryPostDto>
{
    public JobCategoryPostDtoValidator()
    {
        RuleFor(p => p.Name)
            .NotNull().WithMessage("Name is required")
            .NotEmpty()
            .MaximumLength(75);
        RuleFor(p => p.Description)
            .MaximumLength(800);
    }
}
