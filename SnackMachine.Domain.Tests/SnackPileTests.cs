using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SnackMachine.Domain.Tests
{
    [TestClass]
    public class SnackPileTests
    {
        [TestMethod]
        public void Create_SnackIsNull_ThrowsException()
        {
            Func<SnackPile> result = () => new SnackPile(null, 1, 2);

            result.Should().Throw<InvalidOperationException>();
        }

        [TestMethod]
        public void Create_QuantityIsLessThanZero_ThrowsException()
        {
            Func<SnackPile> result = () => new SnackPile(new Snack("Mars"), -3,2);

            result.Should().Throw<InvalidOperationException>();
        }

        [TestMethod]
        public void Create_PriceIsLessThanZero_ThrowsException()
        {
            Func<SnackPile> result = () => new SnackPile(new Snack("Mars"), 1,-2);

            result.Should().Throw<InvalidOperationException>();
        }

        [TestMethod]
        public void Create_PriceIsLessThan1Cent_ThrowsException()
        {
            Func<SnackPile> result = () => new SnackPile(new Snack("Mars"), 1,0.001m);

            result.Should().Throw<InvalidOperationException>();
        }
    }
}
