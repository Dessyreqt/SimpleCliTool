namespace SimpleCliTool.Logic.Program
{
    using System;
    using System.Linq;
    using Data;

    public class ParseCommand : Command
    {
        private readonly string input;

        public ParseCommand(AppState appState, string input) : base(appState)
        {
            this.input = input;
        }

        public override string HelpText => "";

        protected override void Handle()
        {
            string[] tokens;

            try
            {
                tokens = Tokenizer.GetTokens(input);
            }
            catch (ArgumentException)
            {
                AppState.CurrentCommand = new ConsoleCommand { Type = CommandType.None };
                AppState.CurrentOutput = new CommandOutput("", false);
                return;
            }

            var defaultParamCount = 0;

            var consoleCommand = new ConsoleCommand
            {
                Type = GetCommandType(tokens[0])
            };

            if (consoleCommand.Type == CommandType.Ambiguous)
            {
                AppState.CurrentCommand = consoleCommand;
                var matchedCommands = AppState.AvailableCommands.Keys.Where(key => key.StartsWith(tokens[0], StringComparison.InvariantCultureIgnoreCase)).ToList();
                AppState.CurrentOutput = new CommandOutput(string.Join(" ", matchedCommands), false);
                return;
            }

            if (consoleCommand.Type == CommandType.None)
            {
                AppState.CurrentCommand = consoleCommand;
                AppState.CurrentOutput = new CommandOutput("No command found, please check input and try again.");
                return;
            }

            consoleCommand.Command = GetCommandObject(AppState, AppState.AvailableCommands[consoleCommand.Type]);

            for (int i = 1; i < tokens.Length; i++)
            {
                if (tokens[i].StartsWith("-"))
                {
                    if (i != tokens.Length - 1)
                    {
                        consoleCommand.Parameters[tokens[i]] = tokens[i + 1];
                        i++;
                    }
                }
                else
                {
                    consoleCommand.Parameters[$"_{defaultParamCount}"] = tokens[i];
                    defaultParamCount++;
                }
            }

            if (tokens.Length > 1)
            {
                consoleCommand.ParameterString = input.Substring(tokens[0].Length).Trim();
            }

            AppState.CurrentCommand = consoleCommand;
        }

        public static Command GetCommandObject(AppState appState, Type type)
        {
            return (Command)type.GetConstructor(new [] { typeof(AppState) }).Invoke(new object[] { appState });
        }

        private string GetCommandType(string commandName)
        {
            var matchedCommands = AppState.AvailableCommands.Where(com => com.Key.StartsWith(commandName.Trim(), StringComparison.InvariantCultureIgnoreCase)).ToList();

            if (matchedCommands.Count == 0)
            {
                return CommandType.None;
            }

            if (matchedCommands.Count > 1)
            {
                return CommandType.Ambiguous;
            }

            return matchedCommands[0].Key;
        }
    }
}
