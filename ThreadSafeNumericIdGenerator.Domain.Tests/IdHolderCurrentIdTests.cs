using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThreadSafeNumericIdGenerator.Domain.Model.ValueObjects;

namespace ThreadSafeNumericIdGenerator.Domain.Tests
{
    [TestClass]
    public class IdHolderCurrentIdTests
    {
        [TestMethod]
        public void Create_ValueIsNull_ReturnsResultSuccessAndSetValueToZero()
        {
            var result = IdHolderCurrentId.Create(null);

            result.IsSuccess.Should().BeTrue();
            result.Value.Value.Should().Be(0);
        }

        [TestMethod]
        public void Create_ValueIsLessThanZero_ReturnsResultFail()
        {
            var result = IdHolderCurrentId.Create(-3);

            result.IsFailure.Should().BeTrue();
        }

        [TestMethod]
        public void Create_ValueIsLessGreaterZero_ReturnsResultSuccessAndValueSet()
        {
            var result = IdHolderCurrentId.Create(3);

            result.IsSuccess.Should().BeTrue();
            result.Value.Value.Should().Be(3);
        }

        [TestMethod]
        public void Next_ReturnsIncrementValue()
        {
            var result = IdHolderCurrentId.Create(3);
            var idHolderCurrentId = result.Value;

            var next = idHolderCurrentId.Next();

            next.Should().Be(4);
            idHolderCurrentId.Value.Should().Be(4);
        }
    }
}
