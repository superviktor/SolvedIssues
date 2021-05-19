using System.Text.RegularExpressions;
using FluentValidation;

namespace Validation.Api.Validator
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            //class level cascade mode
            //CascadeMode = CascadeMode.Stop;

            //bad practice to use ruleset
            //RuleSet("CustomRuleSet", () =>
            //{
            //    RuleFor(x => x.Name).NotEmpty().Length(0, 10);
            //});

            RuleFor(x => x.Name).NotEmpty().Length(0, 200);
            RuleFor(x => x.Email).NotEmpty().Length(0, 150).EmailAddress();
            RuleFor(x => x.Addresses).NotNull().SetValidator(new AddressesValidator());
             
            //Cascade mode: stop when one check fails
            //RuleFor(x => x.PhoneNumber)
            //    .Cascade(CascadeMode.Stop)
            //    .NotEmpty()
            //    .When(x => x.PhoneNumber != null, ApplyConditionTo.CurrentValidator)
            //    .Must(x => Regex.IsMatch(x, "^[2-9][0-9]{9}$"))
            //    .WithMessage("Phone number is incorrect");

            ////One or both fields should be present
            //When(x => x.Email == null, () =>
            //{
            //    RuleFor(x => x.PhoneNumber).NotEmpty();
            //});
            //When(x => x.PhoneNumber == null, () =>
            //{
            //    RuleFor(x => x.Email).NotEmpty();
            //});
            //RuleFor(x => x.Email)
            //    .NotEmpty()
            //    .Length(0, 150)
            //    .EmailAddress()
            //    .When(x => x.Email != null);
            //RuleFor(x => x.PhoneNumber)
            //    .NotEmpty()
            //    .Matches("^[2-9][0-9]{9}$")
            //    .When(x => x.PhoneNumber != null);

            //Conditional validation  
            //RuleFor(x => x.PhoneNumber)
            //    .NotEmpty()
            //    .When(x => x.PhoneNumber != null, ApplyConditionTo.CurrentValidator)
            //    .Must(x => Regex.IsMatch(x, "^[2-9][0-9]{9}$"))
            //    .WithMessage("Phone number is incorrect");

            //Inheritance validation should be used in domain validation and is bad practice in contract
            //RuleFor(x => x.PhoneNumber).SetInheritanceValidator(v =>
            //{
            //    v.Add<UsPhoneNumber>(new UsPhoneNumberValidator());
            //    v.Add<InternationalPhoneNumber>(new InternationalPhoneNumberValidator())
            //});
        }
    }
}
