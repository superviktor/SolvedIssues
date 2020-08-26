using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThreadSafeNumericIdGenerator.Domain.Model.Entities;
using ThreadSafeNumericIdGenerator.Domain.Model.ValueObjects;

namespace ThreadSafeNumericIdGenerator.Domain.Tests
{
    [TestClass]
    public class IdHolderTests
    {
        [TestMethod]
        public void Next_ReturnsIncrementedValue()
        {
            var idHolderName = IdHolderName.Create("name");
            var idHolderCurrentId = IdHolderCurrentId.Create(1);
            var idHolder = new IdHolder(idHolderName.Value, idHolderCurrentId.Value);

            var next = idHolder.Next();

            next.Should().Be(2);
            idHolderCurrentId.Value.Value.Should().Be(2);
        }
    }
}
