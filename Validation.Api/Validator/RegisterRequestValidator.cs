using FluentValidation;

namespace Validation.Api.Validator
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            //bad practice
            //RuleSet("CustomRuleSet", () =>
            //{
            //    RuleFor(x => x.Name).NotEmpty().Length(0, 10);
            //});
            RuleFor(x => x.Name).NotEmpty().Length(0, 200);
            RuleFor(x => x.Email).NotEmpty().Length(0, 150).EmailAddress();
            RuleFor(x => x.Addresses).NotNull().SetValidator(new AddressesValidator());
            //Inheritance validation should be used in domain validation and is bad practice in contract
            //RuleFor(x => x.PhoneNumber).SetInheritanceValidator(v =>
            //{
            //    v.Add<UsPhoneNumber>(new UsPhoneNumberValidator());
            //    v.Add<InternationalPhoneNumber>(new InternationalPhoneNumberValidator())
            //});
        }
    }
}
