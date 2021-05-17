namespace Validation.Domain
{
    public class Address
    {
        public Address(string street, string city, string postalCode)
        {
            Street = street;
            City = city;
            PostalCode = postalCode;
        }

        public string Street { get; init; }
        public string City { get; init; }
        public string PostalCode { get; init; }
    }
}
