using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace Validation.Domain
{
    public class StudentName : ValueObject
    {
        private StudentName(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static Result<StudentName> Create(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return Result.Failure<StudentName>("Value is required");

            if (input.Trim().Length > 200)
                return Result.Failure<StudentName>("Value is too long");

            return Result.Success(new StudentName(input));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}