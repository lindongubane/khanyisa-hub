using Domain.Model;
using FluentValidation;

namespace Application.Validation;

public class AddressValidator : AbstractValidator<Address>
{
    public AddressValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.City).NotEmpty();
        RuleFor(x => x.Province).NotEmpty();
        RuleFor(x => x.Line1).NotEmpty();
        RuleFor(x => x.Line2).NotEmpty();
        RuleFor(x => x.ZipCode).NotEmpty().GreaterThanOrEqualTo(4);
        RuleFor(x => x.Type).NotEmpty();
    }
}
