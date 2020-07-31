using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace TextParsingWithRegex.Domain
{
    public static class StringExtensions
    {
        public static string GetFirstMatch(this string text, string pattern)
        {
            var firstMatch = Regex.Match(text, pattern);
            var value = firstMatch.Groups[1].Value;

            return value;
        }

        public static string RemoveNewLineCharacters(this string input)
        {
            var result = Regex.Replace(input, @"\t|\n|\r", "");

            return result;
        }

        public static string ReplaceWithSpace(this string input, string pattern)
        {
            var result = Regex.Replace(input, pattern, " ");

            return result;
        }

        public static IEnumerable<string> GetAllMatchesWithIntersection(this string text, string pattern)
        {
            var values = new List<string>();

            void AddMatchToResult(Match matchToAdd)
            {
                if (!string.IsNullOrEmpty(matchToAdd.Groups[1].Value))
                    values.Add(matchToAdd.Groups[1].Value);
            }

            var regex = new Regex(pattern);
            var match = regex.Match(text);
            AddMatchToResult(match);
            while (match.Success)
            {
                match = regex.Match(text, match.Index + 1);
                AddMatchToResult(match);
            }

            return values;
        }

        public static IEnumerable<string> GetAllMatches(this string text, string pattern)
        {
            var matches = Regex.Matches(text, pattern);
            var values = matches.Select(s => s.Groups[1].Value);

            return values;
        }
    }
}
