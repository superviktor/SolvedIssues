using System.Collections.Generic;

namespace EFCorePlusDDD.Api.Domain.Models
{
    public class Name : ValueObject
    {
        private Name(string first, string last, Suffix suffix) : this()
        {
            First = first;
            Last = last;
            Suffix = suffix;
        }

        protected Name() { }

        public string First { get;  }
        public string Last { get; }
        public virtual Suffix Suffix { get; }

        public static Result<Name> Create(string firstName, string lastName, Suffix suffix)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                return Result.Fail<Name>("First name should not be empty");
            if (string.IsNullOrWhiteSpace(lastName))
                return Result.Fail<Name>("Last name should not be empty");

            firstName = firstName.Trim();
            lastName = lastName.Trim();

            if (firstName.Length > 200)
                return Result.Fail<Name>("First name is too long");    
            if (lastName.Length > 200)
                return Result.Fail<Name>("Last name is too long");

            return Result.Success(new Name(firstName, lastName, suffix));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return First;
            yield return Last;
            yield return Suffix;
        }
    }
}