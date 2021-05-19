using FluentValidation;

namespace Validation.Api.Validator
{
    public class AddressesValidator : AbstractValidator<AddressDto[]>
    {
        public AddressesValidator()
        {
            RuleFor(x => x)
                .NotNull()
                .ListMustContainsNumberOfElements(1,2)
                .ForEach(a => 
                {
                    a.NotNull();
                    a.SetValidator(new AddressValidator());
                });
        }
    }
}
