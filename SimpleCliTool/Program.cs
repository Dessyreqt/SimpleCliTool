namespace SimpleCliTool
{
    using System;
    using Cli;
    using Data;
    using Logic;
    using Logic.Program;
    using Logic.Program.Configuration;

    class Program
    {
        static void Main(string[] args)
        {
            var appState = InitializeState();

            do
            {
                Console.Write($"{appState.CurrentEnvironment.Name}> ");

                string input = "";

                while (string.IsNullOrWhiteSpace(input))
                {
                    input = AutoComplete.ReadLine(appState.AvailableCommands.Keys);
                }

                input = HandlePreviousCommandTokens(appState, input);

                Command.Run(new ParseCommand(appState, input));

                if (appState.CurrentCommand.Command != null)
                {
                    Command.Run(appState.CurrentCommand.Command);
                }

                if (appState.CurrentOutput.Success)
                {
                    appState.PreviousOutput = appState.CurrentOutput;
                }

                Console.WriteLine(appState.CurrentOutput.Text);
            }
            while (!appState.Exiting);
        }

        private static string HandlePreviousCommandTokens(AppState appState, string input)
        {
            var previousOutput = appState.PreviousOutput;

            if (previousOutput == null)
            {
                return input;
            }

            for (int i = previousOutput.TextList.Count - 1; i > -1; i--)
            {
                input = input.Replace($"--{i}", previousOutput.TextList[i]);
            }

            return input;
        }

        private static AppState InitializeState()
        {
            var appState = new AppState();

            Command.Run(new GetAvailableCommands(appState));
            Command.Run(new LoadConfigurationCommand(appState));

            if (appState.Configuration.Environments.Count == 0)
            {
                appState.Configuration = Configuration.Default;
            }

            appState.CurrentEnvironment = appState.Configuration.Environments[0];

            return appState;
        }
    }
}