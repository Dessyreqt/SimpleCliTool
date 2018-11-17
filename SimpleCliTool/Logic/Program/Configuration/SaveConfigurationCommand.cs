namespace SimpleCliTool.Logic.Program.Configuration
{
    using System.IO;
    using System.Text;
    using Data;

    public class SaveConfigurationCommand : Command
    {
        public SaveConfigurationCommand(AppState appState) : base(appState)
        {
        }

        public override string HelpText => "";

        protected override void Handle()
        {
            var configText = GetConfigText(AppState.Configuration);

            using (var writer = new StreamWriter(Configuration.Filename))
            {
                writer.Write(configText);
            }
        }

        private static string GetConfigText(Configuration config)
        {
            var retVal = new StringBuilder();

            foreach (var environment in config.Environments)
            {
                AddEnvironmentText(retVal, environment);
                retVal.AppendLine();
            }

            return retVal.ToString();
        }

        private static void AddEnvironmentText(StringBuilder retVal, Data.DataEnvironment environment)
        {
            retVal.AppendLine($"[ENV-{environment.Name}]");
            retVal.AppendLine($"server={environment.Server}");
            retVal.AppendLine($"database={environment.Database}");
        }
    }
}
