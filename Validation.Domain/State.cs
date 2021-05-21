using System.Collections.Generic;
using System.Linq;
using CSharpFunctionalExtensions;

namespace Validation.Domain
{
    public class State : ValueObject
    {
        private State(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static Result<State> Create(string input, string[] allStates)
        {
            if (string.IsNullOrWhiteSpace(input))
                return Result.Failure<State>("Value is required");

            var state = input.Trim().ToUpper();
            if(state.Length != 2)
                return Result.Failure<State>("Value length is invalid");

            if(allStates.Any(x=>x == state) == false)
                return Result.Failure<State>("State is invalid");

            return Result.Success(new State(state));

        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
