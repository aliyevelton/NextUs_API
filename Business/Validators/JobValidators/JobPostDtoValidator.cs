using Business.DTOs.JobDtos;
using FluentValidation;

namespace Business.Validators.JobValidators;

public class JobPostDtoValidator : AbstractValidator<JobPostDto>
{
    public JobPostDtoValidator()
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
        RuleFor(p => p.JobType)
            .NotNull().WithMessage("Job type is required")
            .NotEmpty()
            .MaximumLength(30);
        RuleFor(p => p.Salary)
            .NotEmpty().WithMessage("Salary cannot be empty")
            .InclusiveBetween(100, 10000).When(p => p.Salary != -1);
        RuleFor(p => p.CategoryId)
            .NotNull().WithMessage("Category is required")
            .NotEmpty();
        RuleFor(p => p.CompanyId)
            .NotNull().WithMessage("Company is required")
            .NotEmpty();
    }
}
