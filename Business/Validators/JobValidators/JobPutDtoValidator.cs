using Business.DTOs.JobDtos;
using FluentValidation;

namespace Business.Validators.JobValidators;

public class JobPutDtoValidator : AbstractValidator<JobPutDto>
{
    public JobPutDtoValidator()
    {
        RuleFor(p => p.Title)
            .NotNull().WithMessage("Title is required")
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(75);
        RuleFor(p => p.Position)
            .NotNull()
            .NotEmpty()
            .MaximumLength(75);
        RuleFor(p => p.Description)
            .NotNull().WithMessage("Description is required")
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(5000);
        RuleFor(p => p.Location)
            .NotNull().WithMessage("Location is required")
            .NotEmpty()
            .MaximumLength(30);
    }
}
