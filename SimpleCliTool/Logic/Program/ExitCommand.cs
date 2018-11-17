namespace SimpleCliTool.Logic.Program
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
        }
    }
}
