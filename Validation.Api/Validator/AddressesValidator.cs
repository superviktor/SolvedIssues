using FluentValidation;
using Validation.Domain;

namespace Validation.Api.Validator
{
    public class AddressesValidator : AbstractValidator<AddressDto[]>
    {
        public AddressesValidator(StateRepository stateRepository)
        {
            var allStates = stateRepository.GetAll();
            RuleFor(addresses => addresses)
                .NotNull()
                .ListMustContainsNumberOfElements(1, 2)
                .ForEach(addresses =>
                {
                    addresses.NotNull();
                    addresses.ChildRules(address =>
                    {
                        address.CascadeMode = CascadeMode.Stop;
                        address.RuleFor(y => y.State)
                            .MustBeValueObject(s => State.Create(s, allStates));
                        address.RuleFor(y => y)
                            .MustBeEntity(y => Address.Create(y.Street, y.City, y.PostalCode, y.State, allStates));
                    });
                });
        }
    }
}
