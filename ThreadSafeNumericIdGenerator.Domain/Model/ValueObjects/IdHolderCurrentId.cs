using ThreadSafeNumericIdGenerator.Domain.Base;

namespace ThreadSafeNumericIdGenerator.Domain.Model.ValueObjects
{
    public class IdHolderCurrentId : ValueObject<IdHolderCurrentId>
    {
        private IdHolderCurrentId(long value)
        {
            Value = value;
        }

        public static Result<IdHolderCurrentId> Create(long? value)
        {
            if (value == null)
                value = 0;

            if (value < 0)
                return Result.Fail<IdHolderCurrentId>("From value should be positive number");

            return Result.Success(new IdHolderCurrentId(value.Value));
        }

        public long Value { get; }

        protected override bool EqualsCore(IdHolderCurrentId other)
        {
            return Value.Equals(other.Value);
        }

        protected override int GetHashCodeCore()
        {
            return Value.GetHashCode();
        }

        public static explicit operator IdHolderCurrentId(long startFrom)
        {
            return Create(startFrom).Value;
        }

        public static implicit operator long(IdHolderCurrentId idHolderCurrentId)
        {
            return idHolderCurrentId.Value;
        }
    }
}
