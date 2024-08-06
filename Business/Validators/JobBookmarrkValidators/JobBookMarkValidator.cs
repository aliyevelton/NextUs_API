using Business.DTOs.JobBookmarkDtos;
using FluentValidation;

namespace Business.Validators.JobBookmarrkValidators;

public class JobBookMarkValidator :  AbstractValidator<JobBookmarkPostDto>
{
    public JobBookMarkValidator()
    {
        RuleFor(p => p.JobId)
            .NotNull()
            .WithMessage("Job id is required");
        RuleFor(p => p.UserId)
            .NotNull()
            .WithMessage("User id is required");
    }
}
