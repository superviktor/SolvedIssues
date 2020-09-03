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
            snackMachine.InsertMoney(new Money(0, 1, 0, 1, 0, 0));
            snackMachine.ReturnMoney();

            snackMachine.MoneyInTransaction.Amount.Should().Be(0m);
        }

        [TestMethod]
        public void InsertMoney_IncreasesMoneyInTransaction()
        {
            snackMachine.InsertMoney(Money.Dollar);
            snackMachine.InsertMoney(Money.TenCents);

            snackMachine.MoneyInTransaction.Amount.Should().Be(1.1m);
        }

        [TestMethod]
        public void InsertMoney_CantInsertMoreThanOneCoinOrNoteAtATime()
        {
            var money = Money.Cent + Money.Cent;

            Action result = ()=> snackMachine.InsertMoney(money);

            result.Should().Throw<InvalidOperationException>();
        }

        [TestMethod]
        public void BuySnack_MoneyFromTransactionMovesToMoneyInside()
        {
            snackMachine.InsertMoney(Money.Dollar);
            snackMachine.InsertMoney(Money.Dollar);

            snackMachine.BuySnack();

            snackMachine.MoneyInside.Amount.Should().Be(2);
        }
    }
}