namespace SimpleCliTool.Logic.Program
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Data;

    public class GetAvailableCommands : Command
    {
        public GetAvailableCommands(AppState appState) : base(appState)
        {
        }

        public override string HelpText => "";

        protected override void Handle()
        {
            AppState.AvailableCommands.Clear();
            AppState.AvailableCommands.Add(GetCommandName(typeof(ExitCommand)), typeof(ExitCommand));

            var types = GetCommands($"{nameof(SimpleCliTool)}.{nameof(Logic)}.{nameof(User)}");
            foreach (var type in types)
            {
                AppState.AvailableCommands.Add(GetCommandName(type), type);
            }
        }

        static IEnumerable<Type> GetCommands(string nameSpace)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            return asm.GetTypes().Where(type => type.Namespace != null && type.Namespace.StartsWith(nameSpace) && type.BaseType == typeof(Command) && type.GetConstructor(new[] { typeof(AppState) }) != null);
        }

        private string GetCommandName(Type type)
        {
            return type.Name.Remove(type.Name.Length - "Command".Length);
        }
    }
}
