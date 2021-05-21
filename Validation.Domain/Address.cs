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

        public static Result<Address> Create(string street, string city, string postalCode, string state, string[] allStates)
        {
            street = (street ?? string.Empty).Trim();
            city = (city ?? string.Empty).Trim();
            postalCode = (postalCode ?? string.Empty).Trim();

            var stateObj = State.Create(state, allStates).Value;

            if (street.Length < 1 || street.Length > 100)
                return Result.Failure<Address>("Invalid street length");
            if (city.Length < 1 || city.Length > 40)
                return Result.Failure<Address>("Invalid city length");
            if (postalCode.Length < 1 || postalCode.Length > 5)
                return Result.Failure<Address>("Invalid postal code length");

            return Result.Success(new Address(street, city, postalCode, stateObj));
        }
    }
}
