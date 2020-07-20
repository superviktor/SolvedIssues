using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThreadSafeNumericIdGenerator.Domain.Model.ValueObjects;

namespace ThreadSafeNumericIdGenerator.Domain.Tests
{
    [TestClass]
    public class IdHolderNameTests
    {
        [TestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow(" ")]
        public void Create_NameIsNullOrEmpty_ReturnsResultFail(string name)
        {
            var result = IdHolderName.Create(name);

            result.IsFailure.Should().BeTrue();
        }

        [TestMethod]
        public void Create_NameIsValid_ReturnsObjectWithName()
        {
            var idHolderName = "name";
            var result = IdHolderName.Create(idHolderName);

            result.IsSuccess.Should().BeTrue();
            result.Value.Value.Should().Be(idHolderName);
        }
    }
}
