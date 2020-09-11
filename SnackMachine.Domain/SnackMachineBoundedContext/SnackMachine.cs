using SnackMachine.Domain.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using SnackMachine.Domain.Base;

namespace SnackMachine.Domain.SnackMachineBoundedContext
{
    public sealed class SnackMachine : AggregateRoot
    {
        public SnackMachine()
        {
            MoneyInside = Money.None;
            MoneyInTransaction = 0;
            Slots = new List<Slot>
            {
                new Slot(1, this),
                new Slot(2, this),
                new Slot(3, this)
            };
        }

        public Money MoneyInside { get; private set; }
        public decimal MoneyInTransaction { get; private set; }
        private List<Slot> Slots { get; }

        public void InsertMoney(Money moneyToInsert)
        {
            Money[] possibleToInsert =
                {Money.Cent, Money.TenCents, Money.Quater, Money.Dollar, Money.FiveDollars, Money.TwentyDollars};

            if (!possibleToInsert.Contains(moneyToInsert))
                throw new InvalidOperationException();

            MoneyInTransaction += moneyToInsert.Amount;
            MoneyInside += moneyToInsert;
        }

        public void ReturnMoney()
        {
            var moneyToReturn = MoneyInside.Allocate(MoneyInTransaction);
            MoneyInside -= moneyToReturn;
            MoneyInTransaction = 0;
        }

        public void BuySnack(int position)
        {
            var slot = GetSlot(position);

            if (slot.SnackPile.Price > MoneyInTransaction)
                throw new InvalidOperationException();

            slot.SnackPile = slot.SnackPile.SubtractOne();

            var change = MoneyInside.Allocate(MoneyInTransaction - slot.SnackPile.Price);

            if (change.Amount < MoneyInTransaction - slot.SnackPile.Price)
                throw new InvalidOperationException();

            MoneyInside -= change;

            MoneyInTransaction = 0;

            AddDomainEvent(new SnackBought(slot.Position, slot.SnackPile.Quantity, slot.SnackPile.Snack.Name));
        }

        public Slot GetSlot(int position)
        {
            return Slots.Single(s => s.Position == position);
        }

        public void LoadSnacks(int position, SnackPile snackPile)
        {
            var slot = Slots.Single(s => s.Position == position);
            slot.SnackPile = snackPile;
        }

        public void LoadMoney(Money money)
        {
            MoneyInside = money;
        }
    }
}