using System;
using System.Collections.Generic;
using System.Linq;

namespace SnackMachine.Domain
{
    public sealed class SnackMachine : Entity
    {
        public Money MoneyInside { get; private set; }
        public Money MoneyInTransaction { get; private set; }
        private List<Slot> _slots;
        public IReadOnlyCollection<Slot> Slots => _slots.AsReadOnly();

        public SnackMachine()
        {
            MoneyInside = Money.None;
            MoneyInTransaction = Money.None;
            _slots = new List<Slot>
            {
                new Slot(null,0,0,1,this),
                new Slot(null,0,0,2,this),
                new Slot(null,0,0,3,this),
            };
        }

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

        public void BuySnack(int position)
        {
            _slots.Single(s => s.Position == position).Quantity -= 1;

            MoneyInside += MoneyInTransaction;
            MoneyInTransaction = Money.None;
        }

        public void LoadSnacks(int position, Snack snack, decimal price, int quantity)
        {
            var slot = _slots.Single(s => s.Position == position);
            slot.Snack = snack;
            slot.Price = price;
            slot.Quantity = quantity;
        }
    }
}