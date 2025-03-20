using Domain.Model;
using FluentValidation;

namespace Application.Validation;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("FirstName is required");

        RuleFor(x => x.LastName).NotEmpty().WithMessage("LastName is required");

        RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required").EmailAddress().WithMessage("Invalid email address");

        RuleFor(x => x.Cell).NotEmpty().WithMessage("Cellphone number is required");
    }
}
