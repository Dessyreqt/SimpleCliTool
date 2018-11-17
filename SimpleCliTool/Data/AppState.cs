namespace SimpleCliTool.Data
{
    using System;
    using System.Collections.Generic;

    public class AppState
    {
        public Dictionary<string, Type> AvailableCommands { get; set; }
        public ConsoleCommand CurrentCommand { get; set; }
        public CommandOutput CurrentOutput { get; set; }
        public CommandOutput PreviousOutput { get; set; }
        public Configuration Configuration { get; set; }
        public DataEnvironment CurrentEnvironment { get; set; }

        public AppState()
        {
            AvailableCommands = new Dictionary<string, Type>();
        }
    }
}
