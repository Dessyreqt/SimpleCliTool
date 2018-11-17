namespace SimpleCliTool.Logic.User.Help
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Data;
    using Program;

    public class HelpCommand : Command
    {
        public HelpCommand(AppState appState) : base(appState)
        {
        }

        public override string HelpText => "Displays information about a command.";

        public override List<CommandParameter> Parameters => new List<CommandParameter>
        {
            new CommandParameter("_0", "command", "The command about which to retrieve more information.")
        };

        protected override void Handle()
        {
            List<string> commands;

            if (AppState.CurrentCommand.Parameters.ContainsKey("_0"))
            {
                commands = GetCommands(AppState.CurrentCommand.Parameters["_0"]);
            }
            else
            {
                commands = new List<string> { "Help" };
            }

            if (commands.Count == 0)
            {
                AppState.CurrentOutput = new CommandOutput("Couldn't find that command.", false);
                return;
            }

            if (commands.Count > 1)
            {
                AppState.CurrentOutput = new CommandOutput($"Please specify which command you'd like to get help on: {string.Join(" ", commands)}", false);
                return;
            }

            var commandObject = ParseCommand.GetCommandObject(AppState, AppState.AvailableCommands[commands[0]]);

            AppState.CurrentOutput = new CommandOutput(GetHelpText(commands[0], commandObject));
        }

        private static string GetHelpText(string commandName, Command commandObject)
        {
            var helpText = new StringBuilder();
            helpText.AppendLine($"{commandName} - {commandObject.HelpText}");
            helpText.AppendLine();

            AppendUsageText(helpText, commandName, commandObject);

            return helpText.ToString();
        }

        private static void AppendUsageText(StringBuilder helpText, string commandName, Command commandObject)
        {
            helpText.Append($"Usage: {commandName}");

            var unnamedParameters = commandObject.Parameters.Where(x => x.InternalName.StartsWith("_")).OrderBy(x => x.InternalName).Select(x => x.ExternalName).ToList();
            if (unnamedParameters.Count > 0)
            {
                helpText.Append($" <{string.Join("> <", unnamedParameters)}>");
            }

            var namedParameters = commandObject.Parameters.Where(parameter => parameter.InternalName.StartsWith("-")).Select(x => $"{(x.Required ? "" : "[")}{x.InternalName} <{x.ExternalName}>{(x.Required ? "" : "]")}").ToList();
            if (namedParameters.Count > 0)
            {
                helpText.Append($" <{string.Join(" ", namedParameters)}>");
            }

            helpText.AppendLine();

            if (commandObject.Parameters.Count > 0)
            {
                helpText.AppendLine();
                AppendParameterText(helpText, commandObject);
            }
        }

        private static void AppendParameterText(StringBuilder helpText, Command commandObject)
        {
            helpText.AppendLine("Parameters:");

            foreach (var parameter in commandObject.Parameters.Where(x => x.InternalName.StartsWith("_")).OrderBy(x => x.InternalName))
            {
                helpText.AppendLine($"  <{parameter.ExternalName}> - {parameter.Description}");
            }

            foreach (var parameter in commandObject.Parameters.Where(parameter => parameter.InternalName.StartsWith("-")))
            {
                helpText.AppendLine($"  <{parameter.ExternalName}> - {parameter.Description}");
            }
        }

        public List<string> GetCommands(string input)
        {
            return AppState.AvailableCommands.Keys.Where(key => key.StartsWith(input.Trim(), StringComparison.InvariantCultureIgnoreCase)).ToList();
        }
    }
}
