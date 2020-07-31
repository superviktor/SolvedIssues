using System.Collections.Generic;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TextParsingWithRegex.Domain;

namespace TextParsingWithRegex.Tests
{
    [TestClass]
    public class StringExtensionsTests
    {

        [TestMethod]
        public void GetFirstMatch()
        {
            //Arrange
            var text = "FII+OR+123456789'RFF+AEK:11122233344'RFF+ACD:453099152'MOA+98:5594,09'NAD+PL+++Brrr brr'PRC+8'";

            //Act
            var result = text.GetFirstMatch("RFF\\+ACD:(.*?)'");

            //Assert
            result.Should().Be("453099152");
        }

        [TestMethod]
        public void RemoveNewLineCharacters()
        {
            //Arrange
            var text = "f;lgksdfjgk jstldkglkas jlkafdlhgklasdklgha\r\ngjdkgaudghgjdfsyr fdfiu+545v0 - gskdf ag0f'";

            //Act
            var result = text.RemoveNewLineCharacters();

            //Assert
            result.Should().Be("f;lgksdfjgk jstldkglkas jlkafdlhgklasdklghagjdkgaudghgjdfsyr fdfiu+545v0 - gskdf ag0f'");
        }

        [TestMethod]
        public void ReplaceWithSpace()
        {
            //Arrange
            var text = "This++++++is+++++++++++    complex+name";

            //Act
            var result = text.ReplaceWithSpace("\\++\\s*");

            //Assert
            result.Should().Be("This is complex name");
        }

        [TestMethod]
        public void GetAllMatchesWithIntersection()
        {
            //Arrange
            var text = "START1'RandomText1'START2'RandomText2'START3'RandomText3'";

            //Act
            var result = text.GetAllMatchesWithIntersection("START\\d+'(.*?)(START\\d+'|$)");

            //Assert
            result.Should().BeEquivalentTo(new List<string>
            {
                "RandomText1'",
                "RandomText2'",
                "RandomText3'"
            });
        }

        [TestMethod]
        public void GetAllMatches()
        {
            //Arrange
            var text = "START1'RandomText1END1'START2'RandomText2END2'START3'RandomText3END3'";

            //Act
            var result = text.GetAllMatches("START\\d+'(.*?)END\\d+'");

            //Assert
            result.Should().BeEquivalentTo(new List<string>
            {
                "RandomText1",
                "RandomText2",
                "RandomText3"
            });
        }
    }
}
