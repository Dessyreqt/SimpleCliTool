namespace SimpleCliTool.Logic.Program.Configuration
{
    using System;
    using System.IO;
    using Data;

    public class LoadConfigurationCommand : Command
    {
        public LoadConfigurationCommand(AppState appState) : base(appState)
        {
        }

        public override string HelpText => "";

        protected override void Handle()
        {
            if (File.Exists(Configuration.Filename))
            {
                using (var reader = new StreamReader(Configuration.Filename))
                {
                    var fileText = reader.ReadToEnd();

                    AppState.Configuration = GetConfig(fileText);
                }
            }
            else
            {
                AppState.Configuration = Configuration.Default;
                Run(new SaveConfigurationCommand(AppState));
            }
        }

        private static Configuration GetConfig(string text)
        {
            var lines = text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            DataEnvironment currentEnvironment = null;
            var config = new Configuration();

            foreach (var currentLine in lines)
            {
                var line = currentLine.Trim();

                if (line.StartsWith("[ENV-", StringComparison.InvariantCultureIgnoreCase) && line.EndsWith("]"))
                {
                    if (currentEnvironment != null)
                    {
                        config.Environments.Add(currentEnvironment);
                    }

                    currentEnvironment = new Data.DataEnvironment { Name = line.Substring(5, line.Length - 6) };
                    continue;
                }

                if (currentEnvironment == null)
                {
                    continue;
                }

                if (line.StartsWith("server=", StringComparison.InvariantCultureIgnoreCase))
                {
                    currentEnvironment.Server = line.Substring(7);
                }

                if (line.StartsWith("database=", StringComparison.InvariantCultureIgnoreCase))
                {
                    currentEnvironment.Database = line.Substring(9);
                }
            }

            if (currentEnvironment != null && !config.Environments.Contains(currentEnvironment))
            {
                config.Environments.Add(currentEnvironment);
            }

            return config;
        }

    }
}
