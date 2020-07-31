using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace TextParsingWithRegex.Domain
{
    public static class CremulParser
    {
        const string LinePattern = "LIN\\+\\d+'(.*?)(LIN\\+\\d+'|$)";
        const string SequencePattern = "SEQ\\+\\+\\d+'(.*?)GIS\\+37'";
        const string LineAmountPattern = "MOA\\+\\d\\d:(.*?):[A-Z][A-Z][A-Z]'";
        const string SequenceAmountPattern = "MOA\\+\\d\\d:(.*?)'";
        const string PaymentIdPattern = "RFF\\+ACD:(.*?)'";
        const string KidPattern = "DOC\\+999\\+(.*?)'";
        const string DescriptionPattern = "NAD\\+PL\\++(.*?)'";
        const string PaidDateTimePattern = "DTM\\+\\d\\d\\d:(.*?):\\d\\d\\d'";
        const string BankAccountNumberPaymentReceivedFromPattern = "FII\\+OR\\+(.*?)'";
        const string BankAccountNumberPaymentReceivedToPattern = "FII\\+BF\\++(.*?)'";

        public static IEnumerable<Line> Parse(string input)
        {
            var text = input.RemoveNewLineCharacters();

            var lines = (from lineText in text.GetAllMatchesWithIntersection(LinePattern)
                         let sequences = lineText.GetAllMatches(SequencePattern).Select(sequenceText => new Sequence
                         {
                             PaymentId = GetString(sequenceText, PaymentIdPattern),
                             KID = GetString(sequenceText, KidPattern),
                             Description = GetDescription(sequenceText, DescriptionPattern),
                             Amount = GetDecimal(sequenceText, SequenceAmountPattern),
                             PaidDateTime = GetDateTime(sequenceText, PaidDateTimePattern),
                             BankAccountNumberPaymentReceivedFrom = GetString(sequenceText, BankAccountNumberPaymentReceivedFromPattern),
                             BankAccountNumberPaymentReceivedTo = GetString(lineText, BankAccountNumberPaymentReceivedToPattern)
                         })
                             .ToList()
                         select new Line { Amount = GetDecimal(lineText, LineAmountPattern), Sequences = sequences }).ToList();

            ValidateAmounts(lines);

            return lines;
        }

        private static void ValidateAmounts(IEnumerable<Line> lines)
        {
            if (lines.Any(line => line.Amount != line.Sequences.Sum(s => s.Amount)))
            {
                throw new Exception("Line amount is not equal to sum of sequences amounts");
            }
        }

        private static string GetString(string lineText, string pattern)
        {
            var bankAccountNumberPaymentReceivedTo = lineText.GetFirstMatch(pattern);

            return bankAccountNumberPaymentReceivedTo;
        }

        private static DateTime GetDateTime(string textSequence, string pattern)
        {
            var paidDateTimeText = textSequence.GetFirstMatch(pattern);
            DateTime.TryParseExact(paidDateTimeText, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var paidDateTime);

            return paidDateTime;
        }

        private static decimal GetDecimal(string lineText, string pattern)
        {
            var lineAmountText = lineText.GetFirstMatch(pattern).Replace(',','.');
            decimal.TryParse(lineAmountText, out var lineAmount);

            return lineAmount;
        }

        private static string GetDescription(string textSequence, string pattern)
        {
            var oneOrMorePlusesAndZeroOrMoreSpaces = "\\++\\s*";
            var description = GetString(textSequence, pattern).ReplaceWithSpace(oneOrMorePlusesAndZeroOrMoreSpaces).Trim();

            return description;
        }
    }
}
