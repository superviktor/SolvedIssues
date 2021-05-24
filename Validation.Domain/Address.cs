using CSharpFunctionalExtensions;

namespace Validation.Domain
{
    public class Address : Entity
    {
        private Address(string street, string city, string postalCode, State state)
        {
            Street = street;
            City = city;
            PostalCode = postalCode;
            State = state;
        }

        public string Street { get; init; }
        public string City { get; init; }
        public string PostalCode { get; init; }
        public State State { get; init; }

        public static Result<Address, Error> Create(string street, string city, string postalCode, string state, string[] allStates)
        {
            street = (street ?? string.Empty).Trim();
            city = (city ?? string.Empty).Trim();
            postalCode = (postalCode ?? string.Empty).Trim();

            var stateObj = State.Create(state, allStates).Value;

            if (street.Length < 1 || street.Length > 100)
                return Errors.General.InvalidLength(nameof(street));
            if (city.Length < 1 || city.Length > 40)
                return Errors.General.InvalidLength(nameof(city));
            if (postalCode.Length < 1 || postalCode.Length > 5)
                return Errors.General.InvalidLength(nameof(postalCode));

            return new Address(street, city, postalCode, stateObj);
        }
    }
}
