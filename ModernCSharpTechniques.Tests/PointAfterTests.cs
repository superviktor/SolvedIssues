using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModernCSharpTechniques.Domain.After;

namespace ModernCSharpTechniques.Tests
{
    [TestClass]
    public class PointAfterTests
    {
        [TestMethod]
        public void Equals_PassNull_ReturnsFalse()
        {
            var left = new PointAfter(1, 2);
            object right = null;

            var result = left.Equals(right);

            result.Should().BeFalse();
        }
    }
}
