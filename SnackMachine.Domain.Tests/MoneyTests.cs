using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.ComponentModel;

namespace SnackMachine.Domain.Tests
{
    [TestClass]
    public class MoneyTests
    {
        [TestMethod]
        public void Sum_FirstIsOneDollarSecondIsOneCent_ReturnsOneDolladOneCent()
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
    }
}
