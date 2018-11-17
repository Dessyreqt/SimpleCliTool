namespace SimpleCliTool.Logic.Program
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    public class Tokenizer
    {
        public static string[] GetTokens(string input)
        {
            input = ClearComments(input);

            var tokenList = new List<string>();

            while (!string.IsNullOrWhiteSpace(input))
            {
                var nextToken = GetNextToken(input);

                tokenList.Add(nextToken);

                var removeLength = nextToken.Length;

                if (input.StartsWith("\""))
                {
                    removeLength += 2;
                }

                input = input.Remove(0, removeLength).Trim();
            }

            return tokenList.ToArray();
        }

        private static string GetNextToken(string input)
        {
            input = input.Trim();

            if (input.StartsWith("\""))
            {
                var closingQuoteIndex = input.Substring(1).IndexOf('"');

                if (closingQuoteIndex == -1)
                {
                    throw new ArgumentException("No matching quote found in input.", nameof(input));
                }

                return input.Substring(1, closingQuoteIndex);
            }

            var spaceIndex = input.IndexOf(' ');

            if (spaceIndex == -1)
            {
                return input;
            }

            return input.Substring(0, spaceIndex);
        }

        private static string ClearComments(string input)
        {
            return Regex.Replace(input, @"\/\*.*(\*\/)?", "");
        }
    }
}
