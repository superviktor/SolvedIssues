using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SnackMachine.Domain.SharedKernel;

namespace SnackMachine.Domain.Tests
{
    [TestClass]
    public class MoneyTests
    {
        [TestMethod]
        public void Sum_FirstIsOneDollarSecondIsOneCent_ReturnsOneDollarOneCent()
        {
            var oneDollad = new Money(0, 0, 0, 1, 0, 0);
            var oneCent = new Money(1, 0, 0, 0, 0, 0);

            var sum = oneDollad + oneCent;


            sum.OneCentCount.Should().Be(1);
            sum.TenCentCount.Should().Be(0);
            sum.QuarterCount.Should().Be(0);
            sum.OneDollarCount.Should().Be(1);
            sum.FiveDollarsCount.Should().Be(0);
            sum.TwentyDollarsCount.Should().Be(0);
        }

        [TestMethod]
        public void Equals_TwoInstancesWithSameStructure_AreEqual()
        {
            var money1 = new Money(1, 2, 3, 4, 5, 6);
            var money2 = new Money(1, 2, 3, 4, 5, 6);

            money1.Should().Be(money2);
            money1.GetHashCode().Should().Be(money2.GetHashCode());
        }

        [TestMethod]
        public void Equals_TwoInstancesWithDifferentStructure_AreNotEqual()
        {
            var money1 = new Money(1, 2, 3, 4, 5, 6);
            var money2 = new Money(6, 5, 4, 3, 2, 1);

            money1.Should().NotBe(money2);
            money1.GetHashCode().Should().NotBe(money2.GetHashCode());
        }

        [TestMethod]
        public void Create_PassNegativeAmount_ThrowsException()
        {
            Action action = () => new Money(-1, 0, 0, 0, 0, 0);

            action.Should().Throw<InvalidOperationException>();
        }

        [TestMethod]
        public void Amount_ReturnsCorrectValue()
        {
            var money = new Money(1, 2, 3, 4, 5, 6);

            var amount = money.Amount;

            amount.Should().Be(149.96m);
        }

        [TestMethod]
        public void Subtract_ReturnsCorrectValue()
        {
            var money1 = new Money(8, 7, 6, 5, 4, 3);
            var money2 = new Money(1, 2, 3, 4, 4, 1);

            var result = money1 - money2;

            result.Should().Be(new Money(7, 5, 3, 1, 0, 2));
        }

        [TestMethod]
        public void Subtract_SubtractOneDollarFromOneCent_ThrowsException()
        {
            var money1 = new Money(1, 0, 0, 0, 0, 0);
            var money2 = new Money(0, 0, 1, 0, 0, 0);

            Func<Money> result = () => money1 - money2;

            result.Should().Throw<InvalidOperationException>();
        }
        
        [TestMethod]
        public void Allocate_SixDollars_ReturnsFiveDollarNoteAndOneDollarNote()
        {
            var money = new Money(10, 10, 10, 10, 10, 10);

           var result = money.Allocate(6);

            result.FiveDollarsCount.Should().Be(1);
            result.OneDollarCount.Should().Be(1);
            result.QuarterCount.Should().Be(0);
            result.TenCentCount.Should().Be(0);
            result.OneCentCount.Should().Be(0);
        }

        [TestMethod]
        public void Multiply_x5_ReturnsAllCountsMultipliedByFive()
        {
            var money = new Money(1,2,3,4,5,6);
            var result = money * 5;


            result.OneCentCount.Should().Be(5);
            result.TenCentCount.Should().Be(10);
            result.QuarterCount.Should().Be(15);
            result.OneDollarCount.Should().Be(20);
            result.FiveDollarsCount.Should().Be(25);
            result.TwentyDollarsCount.Should().Be(30);
        }
    }
}