namespace SimpleCliTool.Cli
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class AutoComplete
    {
        public static string ReadLine(IEnumerable<string> autoCompleteSource)
        {
            ConsoleKeyInfo input;

            var suggestions = new List<string>();
            var suggestionIndex = -1;
            var userInput = string.Empty;
            var startingLeft = Console.CursorLeft;
            var startingTop = Console.CursorTop;

            while (ConsoleKey.Enter != (input = Console.ReadKey()).Key)
            {
                if (input.Key == ConsoleKey.Backspace)
                {
                    userInput = userInput.Any() ? userInput.Remove(userInput.Length - 1, 1) : string.Empty;

                    suggestionIndex = -1;
                    suggestions = autoCompleteSource.Where(item => item.StartsWith(userInput, StringComparison.InvariantCultureIgnoreCase)).OrderBy(item => item).ToList();
                }
                else if (input.Key == ConsoleKey.Tab)
                {
                    if ((input.Modifiers & ConsoleModifiers.Shift) == 0)
                    {
                        suggestionIndex++;
                    }
                    else
                    {
                        suggestionIndex--;
                    }

                    if (suggestionIndex >= suggestions.Count)
                    {
                        suggestionIndex = 0;
                    }

                    if (suggestionIndex < 0)
                    {
                        suggestionIndex = suggestions.Count - 1;
                    }

                    userInput = suggestions[suggestionIndex];
                }
                else if (!IsPrintable(input.KeyChar))
                {
                    continue;
                }
                else
                {
                    userInput += input.KeyChar;

                    suggestionIndex = -1;
                    suggestions = autoCompleteSource.Where(item => item.StartsWith(userInput, StringComparison.InvariantCultureIgnoreCase)).OrderBy(item => item).ToList();
                }

                ClearCurrentConsoleLine(startingLeft, startingTop, userInput);

                Console.Write(userInput);
            }

            Console.WriteLine();

            return userInput;
        }

        private static bool IsPrintable(char ch)
        {
            var isLetterOrDigit = char.IsLetterOrDigit(ch);
            var isPunctuation = char.IsPunctuation(ch);
            var isWhiteSpace = char.IsWhiteSpace(ch);

            return isLetterOrDigit || isPunctuation || isWhiteSpace;
        }

        private static void ClearCurrentConsoleLine(int startingLeft, int startingTop, string userInput)
        {
            var clearWidth = Math.Max(Console.WindowWidth - startingLeft, userInput.Length);

            Console.SetCursorPosition(startingLeft, startingTop);
            Console.Write(new string(' ', clearWidth));
            Console.SetCursorPosition(startingLeft, startingTop);
        }
    }
}