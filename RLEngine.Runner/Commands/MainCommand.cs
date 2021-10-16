using RLEngine.Input;

using System.CommandLine;

namespace RLEngine.Runner
{
    public class MainCommand
    {
        private readonly RootCommand rootCommand;
        private PlayerInput? input = null;
        private bool exit = false;

        public MainCommand()
        {
            rootCommand = new()
            {
                new ExitCommand(() => exit = false).CreateDefinition(),
            };
        }

        public bool Execute(string args, out PlayerInput? playerInput)
        {
            input = null;
            exit = false;

            rootCommand.Invoke(args);

            playerInput = input;
            return playerInput is not null || exit;
        }
    }
}