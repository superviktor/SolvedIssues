using System;
using System.Linq;

namespace SnackMachine.Domain
{
    public sealed class SnackMachine : Entity
    {
        public Money MoneyInside { get; private set; } = Money.None;
        public Money MoneyInTransaction { get; private set; } = Money.None;

        public void InsertMoney(Money moneyToInsert)
        {
            Money[] possibleToInsert =
                {Money.Cent, Money.TenCents, Money.Quater, Money.Dollar, Money.FiveDollars, Money.TwentyDollars};

            if (!possibleToInsert.Contains(moneyToInsert))
                throw new InvalidOperationException();

            MoneyInTransaction += moneyToInsert;
        }

        public void ReturnMoney()
        {
            MoneyInTransaction = Money.None;
        }

        public void BuySnack()
        {
            MoneyInside += MoneyInTransaction;
            MoneyInTransaction = Money.None;
        }
    }
}