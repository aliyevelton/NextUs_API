using Business.DTOs.CompanyDTOs;
using FluentValidation;

namespace Business.Validators.CompanyValidators;

public class CompanyValidator : AbstractValidator<CompanyPostDto>
{
    public CompanyValidator()
    {
        RuleFor(p => p.Name)
            .NotNull().WithMessage("Name is required")
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(75);
        RuleFor(p => p.About)
            .MaximumLength(3000);
        //RuleFor(p => p.Logo)
        //    .MaximumLength(255);
        //RuleFor(p => p.CoverImage)
        //    .MaximumLength(255);
        RuleFor(p => p.Website)
            .MaximumLength(50);
        RuleFor(p => p.Email)
            .NotNull().WithMessage("Email is required")
            .NotEmpty()
            .MaximumLength(50)
            .EmailAddress();
        RuleFor(p => p.Phone)
            .NotNull().WithMessage("Phone is required")
            .NotEmpty()
            .Matches(@"^\+?(\d[\d-. ]+)?(\([\d-. ]+\))?[\d-. ]+\d$");
    }
}
