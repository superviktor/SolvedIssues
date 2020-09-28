using System.Collections.Generic;
using System.Linq;
using AdvancedRestfulConcerns.Api.Helpers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdvancedRestfulConcerns.Tests
{
    [TestClass]
    public class PagedListTests
    {
        [TestMethod]
        public void Create_ReturnsExpected()
        {
            var strings = new List<string>
            {
                "1", "2", "3", "4", "5", "6", "7"
            };

            var pagedList = PagedList<string>.Create(strings.AsQueryable(), 2, 2);

            pagedList.Should().BeEquivalentTo(new List<string>
            {
                "3", "4"
            });
        }
    }
}
