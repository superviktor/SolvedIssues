using System;
using System.Collections.Generic;
using System.IO;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TextParsingWithRegex.Domain;

namespace TextParsingWithRegex.Tests
{
    [TestClass]
    public class CremulParserTests
    {
        [TestMethod]
        public void Parse_ReturnsExpected()
        {
            //Arrange
            var text = File.ReadAllText(@".\Resources\cremul.txt");
            var expected = new List<Line>
            {
                new Line
                {
                    Amount = (decimal) (5594.09 + 139.23),
                    Sequences = new List<Sequence>
                    {
                        new Sequence
                        {
                            PaymentId = "453099152",
                            KID = "00000000000001",
                            Description = "Company Company",
                            Amount = (decimal) 5594.09,
                            PaidDateTime = new DateTime(2020,06,09),
                            BankAccountNumberPaymentReceivedFrom ="00000000001",
                            BankAccountNumberPaymentReceivedTo = "00000000002"
                        },
                        new Sequence
                        {
                            PaymentId = "180299343",
                            KID = "00000000000002",
                            Description = "Jonny Haupr",
                            Amount = (decimal) 139.23,
                            PaidDateTime = new DateTime(2020,06,09),
                            BankAccountNumberPaymentReceivedFrom ="00000000003",
                            BankAccountNumberPaymentReceivedTo = "00000000002"
                        }
                    }
                },
                new Line
                {
                    Amount = (decimal) 6484.63,
                    Sequences = new List<Sequence>
                    {
                        new Sequence
                        {
                            PaymentId = "860104831",
                            KID = "00000000000003",
                            Description = "XYZK KAPITAL AS XXXX 0119 NO",
                            Amount = (decimal) 6484.63,
                            PaidDateTime = new DateTime(2020,06,10),
                            BankAccountNumberPaymentReceivedFrom ="00000000005",
                            BankAccountNumberPaymentReceivedTo = "00000000004"
                        }
                    }
                }
            };

            //Act
            var lines = CremulParser.Parse(text);

            //Assert
            lines.Should().BeEquivalentTo(expected);
        }
    }
}
