using Business.DTOs.JobApplicationDtos;
using FluentValidation;

namespace Business.Validators.JobApplicationValidator;

public class JobApplicationValidator : AbstractValidator<JobApplicationPostDto>
{
    public JobApplicationValidator() {
        RuleFor(p => p.FullName)
            .NotNull().WithMessage("Full name is required")
            .NotEmpty()
            .MaximumLength(100);
        RuleFor(p => p.CoverLetter)
            .NotNull().WithMessage("Cover letter is required")
            .NotEmpty()
            .MaximumLength(5000);
        RuleFor(p => p.Cv)
            .NotNull()
            .WithMessage("CV is required");
        RuleFor(p => p.JobId)
            .NotNull()
            .WithMessage("Job id is required");
        RuleFor(p => p.UserId)
            .NotNull()
            .WithMessage("User id is required");
    }
}
