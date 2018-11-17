namespace SimpleCliTool.Data
{
    using System.Collections.Generic;

    public class DataEnvironment
    {
        public string Name { get; set; }
        public string Server { get; set; }
        public string Database { get; set; }
    }

    public class Configuration
    {
        public static readonly string Filename = "config.ini";

        public List<DataEnvironment> Environments { get; set; }

        public Configuration()
        {
            Environments = new List<DataEnvironment>();
        }

        public static Configuration Default => new Configuration
        {
            Environments = new List<DataEnvironment>
            {
                new DataEnvironment { Name = "Local", Server = "localhost", Database = "localdb" },
            }
        };
    }
}
