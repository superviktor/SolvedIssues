﻿using System.Collections.Generic;
using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;

namespace Validation.Domain
{
    public class Email : ValueObject
    {
        private Email(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static Result<Email> Create(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return Result.Failure<Email>("Value is required");

            if (Regex.IsMatch(input, @"^(.+)@(.+)$") == false)
                return Result.Failure<Email>("Value is invalid email");

            return Result.Success(new Email(input));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
