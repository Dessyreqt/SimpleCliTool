namespace SimpleCliTool.Logic.User
{
    using Data;

    public class ExitCommand : Command
    {
        public ExitCommand(AppState appState) : base(appState)
        {
        }

        public override string HelpText => "Exits the program.";

        protected override void Handle()
        {
            AppState.Exiting = true;
            AppState.CurrentOutput = new CommandOutput("Exiting...");
        }
    }
}
