namespace SimpleCliTool.Logic.User.Help
{
    using System;
    using System.Linq;
    using Data;

    public class ListCommandsCommand : Command
    {
        public ListCommandsCommand(AppState appState) : base(appState)
        {
        }

        public override string HelpText => "Lists all commands available.";

        protected override void Handle()
        {
            AppState.CurrentOutput = new CommandOutput(string.Join(Environment.NewLine, AppState.AvailableCommands.Keys.OrderBy(x => x)));
        }
    }
}
