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

        public int OneCentCount { get; private set; }
        public int TenCentCount { get; private set; }
        public int QuarterCount { get; private set; }
        public int OneDollarCount { get; private set; }
        public int FiveDollarsCount { get; private set; }
        public int TwentyDollarsCount { get; private set; }


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