using CSharpObserveVersions.Deconstruction;
using FluentAssertions;
using NUnit.Framework;

namespace CSharpObserveVersions.Tests
{
    [TestFixture]
    public class DeconstructionTests
    {
        [Test]
        public void CanDeconstruct()
        {
            var person = new Person("viktor", 25);

            (string name, int age) = person;

            name.Should().Be(person.Name);
            age.Should().Be(person.Age);
        }
    }
}
