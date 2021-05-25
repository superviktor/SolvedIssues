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

        public static Result<State, Error> Create(string input, string[] allStates)
        {
            if (string.IsNullOrWhiteSpace(input))
                return Errors.General.ValueIsRequired();

            var state = input.Trim().ToUpper();
            if(state.Length != 2)
                return Errors.General.InvalidLength(state);

            if (allStates.Any(x => x == state) == false)
                return Errors.General.ValueIsInvalid();

            return new State(state);

        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
