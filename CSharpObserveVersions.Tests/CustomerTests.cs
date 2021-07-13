using System;
using System.Collections.Generic;
using CSharpObserveVersions.IntefaceDefaultMethods;
using FluentAssertions;
using NUnit.Framework;

namespace CSharpObserveVersions.Tests
{
    [TestFixture]
    public class CustomerTests
    {
        [Test]
        public void TestInterfaceDefaultMethod()
        {
            var sampleCustomer = new SampleCustomer(DateTime.UtcNow.AddYears(-1), "viktor");

            ICustomer iCustomer = sampleCustomer;
            var result = iCustomer.ComputeLoyaltyDiscount();

            result.Should().Be(0);
        }

        [Test]
        public void TestInterfaceDefaultMethodOverload()
        {
            var anotherCustomer = new AnotherCustomer(DateTime.UtcNow.AddYears(-1), "viktor", new List<IOrder>());

            var result = anotherCustomer.ComputeLoyaltyDiscount();

            result.Should().Be(0.5m);
        }
    }
}