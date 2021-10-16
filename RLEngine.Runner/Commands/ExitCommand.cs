using System;
using System.Reflection;
using System.CommandLine;
using System.CommandLine.Invocation;

namespace RLEngine.Runner
{
    public class ExitCommand : Subcommand
    {
        private readonly Action exitCallback;

        public ExitCommand(Action exitCallback)
        : base(_ => { })
        {
            this.exitCallback = exitCallback;
        }

        public override Command CreateDefinition()
        {
            var command = new Command("exit");

            command.AddAlias("e");
            const BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic;
            var method = GetType().GetMethod(nameof(Execute), bindingFlags);
            command.Handler = CommandHandler.Create(method);

            return command;
        }

        private void Execute()
        {
            exitCallback.Invoke();
        }
    }
}