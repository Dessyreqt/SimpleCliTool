namespace SimpleCliTool.Logic.User
{
    using System.Diagnostics;
    using Data;

    public class OpenConfigurationCommand : Command
    {
        public OpenConfigurationCommand(AppState appState) : base(appState)
        {
        }

        public override string HelpText => "Opens the configuration file.";

        protected override void Handle()
        {
            AppState.CurrentOutput = new CommandOutput($"Opening {Configuration.Filename}...");
            Process.Start(Configuration.Filename);
        }
    }
}
