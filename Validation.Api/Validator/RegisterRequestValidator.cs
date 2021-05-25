using FluentValidation;
using Validation.Domain;

namespace Validation.Api.Validator
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator(StateRepository stateRepository)
        {
            Transform(x => x.Name, x => (x ?? string.Empty).Trim())
                .NotEmpty()
                .Length(0, 200);

            RuleFor(x => x.Email)
                .MustBeValueObject(Email.Create)
                .When(x => x.Email != null);

            RuleFor(x => x.Addresses)
                .NotNull()
                .SetValidator(new AddressesValidator(stateRepository));
        }
    }
}
