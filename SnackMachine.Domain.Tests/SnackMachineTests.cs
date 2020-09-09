using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SnackMachine.Domain.Tests
{
    [TestClass]
    public class SnackMachineTests
    {
        private SnackMachine snackMachine;

        [TestInitialize]
        public void Init()
        {
            snackMachine = new SnackMachine();
        }

        [TestMethod]
        public void ReturnMoney_MoneyInTransactionZero()
        {
            snackMachine.InsertMoney(Money.Quater);
            snackMachine.ReturnMoney();

            snackMachine.MoneyInTransaction.Should().Be(0m);
        }

        [TestMethod]
        public void InsertMoney_IncreasesMoneyInTransaction()
        {
            snackMachine.InsertMoney(Money.Dollar);
            snackMachine.InsertMoney(Money.TenCents);

            snackMachine.MoneyInTransaction.Should().Be(1.1m);
        }

        [TestMethod]
        public void InsertMoney_CantInsertMoreThanOneCoinOrNoteAtATime()
        {
            var money = Money.Cent + Money.Cent;

            Action result = () => snackMachine.InsertMoney(money);

            result.Should().Throw<InvalidOperationException>();
        }

        [TestMethod]
        public void BuySnack_MoneyFromTransactionMovesToMoneyInside()
        {
            snackMachine.LoadSnacks(1, new SnackPile(Snack.Chocolate, 10, 1));
            snackMachine.InsertMoney(Money.Dollar);

            snackMachine.BuySnack(1);

            snackMachine.MoneyInside.Amount.Should().Be(1);
        }

        [TestMethod]
        public void BuySnack_EnoughMoneyAndSlotIsNotEmpty_DecreaseSlotQuantityAndIncreaseMoneyInside()
        {
            snackMachine.LoadSnacks(1, new SnackPile(Snack.Chocolate, 10, 1));
            snackMachine.InsertMoney(Money.Dollar);

            snackMachine.BuySnack(1);

            snackMachine.MoneyInTransaction.Should().Be(0);
            snackMachine.MoneyInside.Should().Be(Money.Dollar);
            snackMachine.GetSlot(1).SnackPile.Quantity.Should().Be(9);
        }

        [TestMethod]
        public void BySnack_SlotIsEmpty_ThrowsException()
        {
            snackMachine.LoadSnacks(1, new SnackPile(Snack.Chocolate, 1, 1));
            snackMachine.InsertMoney(Money.Dollar);
            snackMachine.BuySnack(1);
            snackMachine.InsertMoney(Money.Dollar);

            Action result2 = () => snackMachine.BuySnack(1);

            result2.Should().Throw<InvalidOperationException>();
        }

        [TestMethod]
        public void BySnackCostsTwoDollars_InsertOneDollar_ThrowsException()
        {
            snackMachine.LoadSnacks(1, new SnackPile(Snack.Chocolate, 1, 2));

            Action result2 = () => snackMachine.BuySnack(1);

            result2.Should().Throw<InvalidOperationException>();
        }

        [TestMethod]
        public void ReturnMoney_InsertOneDollar_ReturnLargestDenomination()
        {
            snackMachine.LoadMoney(Money.Dollar);
            snackMachine.InsertMoney(Money.Quater);
            snackMachine.InsertMoney(Money.Quater);
            snackMachine.InsertMoney(Money.Quater);
            snackMachine.InsertMoney(Money.Quater);

            snackMachine.ReturnMoney();

            snackMachine.MoneyInside.QuarterCount.Should().Be(4);
            snackMachine.MoneyInside.OneDollarCount.Should().Be(0);
        }

        [TestMethod]
        public void BuySnackOfHalfDollarPrice_InsertOneDollar_ReturnChange()
        {
            snackMachine.LoadSnacks(1, new SnackPile(Snack.Chocolate, 10, 0.5m));
            snackMachine.LoadMoney(Money.Dollar);
            for (var i = 0; i < 10; i++)
            {
                snackMachine.InsertMoney(Money.TenCents);
            }

            snackMachine.BuySnack(1);

            snackMachine.MoneyInside.Amount.Should().Be(1.5m);
            snackMachine.MoneyInTransaction.Should().Be(0);
        }

        [TestMethod]
        public void BuySnack_MachineHasNoChange_ThrowsException()
        {
            snackMachine.LoadSnacks(1, new SnackPile(Snack.Chocolate, 10, 0.5m));
            snackMachine.InsertMoney(Money.Dollar);

            Action result = () => snackMachine.BuySnack(1);

            result.Should().Throw<InvalidOperationException>();
        }
    }
}