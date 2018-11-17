namespace SimpleCliTool.Logic
{
    using System.Collections.Generic;
    using System.Linq;
    using Data;

    public abstract class Command
    {
        protected readonly AppState AppState;

        protected Command(AppState appState)
        {
            AppState = appState;
        }

        public static void Run(Command command)
        {
            foreach (var parameter in command.Parameters)
            {
                if (parameter.Required && !command.AppState.CurrentCommand.Parameters.ContainsKey(parameter.InternalName))
                {
                    command.AppState.CurrentOutput = new CommandOutput("Missing required parameter!");
                }
            }

            command.Handle();
        }

        public abstract string HelpText { get; }
        public virtual List<CommandParameter> Parameters => new List<CommandParameter>();

        protected abstract void Handle();
    }
}
