namespace SimpleCliTool.Logic
{
    public class CommandParameter
    {
        public string InternalName { get; set; }
        public string ExternalName { get; set; }
        public string Description { get; set; }
        public bool Required { get; set; }

        public CommandParameter(string internalName, string externalName, string description, bool required = false)
        {
            InternalName = internalName;
            ExternalName = externalName;
            Description = description;
            Required = required;
        }
    }
}
