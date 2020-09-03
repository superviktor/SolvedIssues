using System;

namespace SnackMachine.Domain
{
    public sealed class Money : ValueObject<Money>
    {
        public Money(
            int oneCentCount, 
            int tenCentCount, 
            int quarterCount, 
            int oneDollarCount, 
            int fiveDollarsCount, 
            int twentyDollarsCount)
        {
            if(oneCentCount < 0 || tenCentCount < 0 || quarterCount < 0 || oneDollarCount < 0 || fiveDollarsCount < 0 || twentyDollarsCount < 0)
                throw new InvalidOperationException();

            OneCentCount = oneCentCount;
            TenCentCount = tenCentCount;
            QuarterCount = quarterCount;
            OneDollarCount = oneDollarCount;
            FiveDollarsCount = fiveDollarsCount;
            TwentyDollarsCount = twentyDollarsCount;
        }

        public static readonly Money None = new Money(0, 0, 0, 0, 0, 0);
        public static readonly Money Cent = new Money(1, 0, 0, 0, 0, 0);
        public static readonly Money TenCents = new Money(0, 1, 0, 0, 0, 0);
        public static readonly Money Quater = new Money(0, 0, 1, 0, 0, 0);
        public static readonly Money Dollar = new Money(0, 0, 0, 1, 0, 0);
        public static readonly Money FiveDollars = new Money(0, 0, 0, 0, 1, 0);
        public static readonly Money TwentyDollars = new Money(0, 0, 0, 0, 0, 1);

        public int OneCentCount { get; }
        public int TenCentCount { get; }
        public int QuarterCount { get; }
        public int OneDollarCount { get; }
        public int FiveDollarsCount { get; }
        public int TwentyDollarsCount { get; }

        public decimal Amount =>
            OneCentCount * 0.01m + TenCentCount * 0.1m + QuarterCount * 0.25m + OneDollarCount + FiveDollarsCount * 5 +
            TwentyDollarsCount * 20;


        public static Money operator +(Money money1, Money money2)
        {
            return new Money(
                money1.OneCentCount + money2.OneCentCount,
                money1.TenCentCount + money2.TenCentCount,
                money1.QuarterCount + money2.QuarterCount,
                money1.OneDollarCount + money2.OneDollarCount,
                money1.FiveDollarsCount + money2.FiveDollarsCount,
                money1.TwentyDollarsCount + money2.TwentyDollarsCount);
        }
        public static Money operator -(Money money1, Money money2)
        {
            return new Money(
                money1.OneCentCount - money2.OneCentCount,
                money1.TenCentCount - money2.TenCentCount,
                money1.QuarterCount - money2.QuarterCount,
                money1.OneDollarCount - money2.OneDollarCount,
                money1.FiveDollarsCount - money2.FiveDollarsCount,
                money1.TwentyDollarsCount - money2.TwentyDollarsCount);
        }

        protected override bool EqualsCore(Money other)
        {
            return OneCentCount == other.OneCentCount &&
                   TenCentCount == other.TenCentCount &&
                   QuarterCount == other.QuarterCount &&
                   OneDollarCount == other.OneDollarCount &&
                   FiveDollarsCount == other.FiveDollarsCount &&
                   TwentyDollarsCount == other.TwentyDollarsCount;
        }

        protected override int GetHashCodeCore()
        {
            var hashCode = OneCentCount;
            hashCode = (hashCode * 397) ^ TenCentCount;
            hashCode = (hashCode * 397) ^ QuarterCount;
            hashCode = (hashCode * 397) ^ OneDollarCount;
            hashCode = (hashCode * 397) ^ FiveDollarsCount;
            hashCode = (hashCode * 397) ^ TwentyDollarsCount;

            return hashCode;
        }
    }
}