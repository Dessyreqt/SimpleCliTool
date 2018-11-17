namespace SimpleCliTool.Data
{
    using System.Collections.Generic;
    using Logic;

    public class ConsoleCommand
    {
        public string Type { get; set; }
        public Dictionary<string, string> Parameters { get; set; }
        public string ParameterString { get; set; }
        public Command Command { get; set; }

        public ConsoleCommand()
        {
            Parameters = new Dictionary<string, string>();
        }
    }
}
