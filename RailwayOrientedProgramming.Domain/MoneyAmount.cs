using RailwayOrientedProgramming.Common;

namespace RailwayOrientedProgramming.Domain
{
    public class MoneyAmount : ValueObject<MoneyAmount>
    {
        public decimal Value { get; }

        private MoneyAmount(decimal value)
        {
            Value = value;
        }

        public static Result<MoneyAmount> Create(decimal value)
        {
            return value <= 0 ? Result.Fail<MoneyAmount>("Can't create with negative value") : Result.Success(new MoneyAmount(value));
        }

        protected override bool EqualsCore(MoneyAmount other)
        {
            return Value == other.Value;
        }

        protected override int GetHashCodeCore()
        {
            return base.GetHashCode();
        }
    }
}
