using Business.DTOs.CourseCategoryDtos;
using FluentValidation;

namespace Business.Validators.CourseCategoryValidators;

public class CourseCategoryPostDtoValidator : AbstractValidator<CourseCategoryPostDto>
{
    public CourseCategoryPostDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .NotNull()
            .MaximumLength(75);
        RuleFor(x => x.Description)
            .MaximumLength(800);
    }
}
