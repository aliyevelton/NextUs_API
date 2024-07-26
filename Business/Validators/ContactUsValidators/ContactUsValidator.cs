using Core.Entities;
using FluentValidation;

namespace Business.Validators.ContactUsValidators;

public class ContactUsValidator : AbstractValidator<ContactUs>
{
    public ContactUsValidator()
    {
        RuleFor(p => p.Name)
            .NotNull().WithMessage("Name is required")
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(50);
        RuleFor(p => p.Surname)
            .NotNull().WithMessage("Surname is required")
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(50);
        RuleFor(p => p.Email)
            .NotNull().WithMessage("Email is required")
            .NotEmpty()
            .MaximumLength(50)
            .EmailAddress();
        RuleFor(p => p.Message)
            .NotNull().WithMessage("Message is required")
            .NotEmpty()
            .MaximumLength(1000);
    }
}
