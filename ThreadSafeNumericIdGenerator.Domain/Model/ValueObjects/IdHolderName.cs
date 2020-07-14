using System;
using ThreadSafeNumericIdGenerator.Domain.Base;

namespace ThreadSafeNumericIdGenerator.Domain.Model.ValueObjects
{
    public class IdHolderName : ValueObject<IdHolderName>
    {
        private IdHolderName(string value)
        {
            Value = value;
        }

        public static Result<IdHolderName> Create(string name)
        {
            if (string.IsNullOrEmpty(name))
                return Result.Fail<IdHolderName>("Name can't be null or empty");

            return Result.Success(new IdHolderName(name));
        }
        public string Value { get; }

        protected override bool EqualsCore(IdHolderName other)
        {
            return Value.Equals(other.Value, StringComparison.InvariantCulture);
        }

        protected override int GetHashCodeCore()
        {
            return Value.GetHashCode();
        }

        public static explicit operator IdHolderName(string name)
        {
            return Create(name).Value;
        }

        public static implicit operator string(IdHolderName idHolderName)
        {
            return idHolderName.Value;
        }
    }
}
