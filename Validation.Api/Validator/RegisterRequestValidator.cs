using FluentValidation;
using Validation.Domain;

namespace Validation.Api.Validator
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MustBeValueObject(StudentName.Create)
                .When(x => x.Name != null);

            RuleFor(x => x.Email)
                .NotEmpty()
                .MustBeValueObject(Email.Create)
                .When(x=>x.Email != null);

            RuleFor(x => x.Addresses)
                .NotNull()
                .SetValidator(new AddressesValidator());
        }
    }
}
