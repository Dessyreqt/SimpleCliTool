namespace SimpleCliTool.Logic
{
    public class CommandParameter
    {
        public string InternalName { get; set; }
        public string ExternalName { get; set; }
        public string Description { get; set; }
        public bool Required { get; set; }

        /// <summary>
        /// Creates a CommandParameter used for validation and documentation purposes.
        /// </summary>
        /// <param name="internalName">The parameter key in AppState.CurrentCommand.Parameters.</param>
        /// <param name="externalName">The friendly name of the parameter as it will appear in documentation.</param>
        /// <param name="description">The description of the parameter.</param>
        /// <param name="required">True if the parameter should be required, false if not.</param>
        public CommandParameter(string internalName, string externalName, string description, bool required = false)
        {
            InternalName = internalName;
            ExternalName = externalName;
            Description = description;
            Required = required;
        }
    }
}
