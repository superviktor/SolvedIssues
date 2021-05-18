using FluentValidation;

namespace Validation.Api.Validator
{
    public class EditPersonalInfoRequestValidator : AbstractValidator<EditPersonalInfoRequest>
    {
        public EditPersonalInfoRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().Length(0, 200);
            RuleFor(x => x.Addresses).NotNull().SetValidator(new AddressesValidator());
        }
    }
}
