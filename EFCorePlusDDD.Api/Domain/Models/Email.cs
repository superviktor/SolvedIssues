using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace EFCorePlusDDD.Api.Domain.Models
{
    public class Email : ValueObject
    {
        private Email(string value) : this()
        {
            Value = value;
        }

        protected Email(){}

        public static Result<Email> Create(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return Result.Fail<Email>("Emails should not be empty");

            email = email.Trim();

            if (email.Length > 200)
                return Result.Fail<Email>("Email is too long");

            if (!Regex.IsMatch(email, @"^(.+)@(.+)$"))
                return Result.Fail<Email>("Email is invalid");

            return Result.Success(new Email(email));
        }

        public string Value { get; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static implicit operator string(Email email)
        {
            return email.Value;
        }
    }
}