using RLEngine.Input;

using CommandLine;

namespace RLEngine.Runner
{
    public static class MovementCommand
    {
        private static readonly string[] aliases = { "move", "m" };

        private class Args
        {
            [Value(0, Required = true)]
            public string Direction { get; set; } = string.Empty;
        }

        public static bool Execute(string command, string[] argsStr, out IPlayerInput? input)
        {
            input = null;
            if (!AliasesUtils.Accepts(command, aliases)) return false;

            var valid = false;
            IPlayerInput? newInput = null;
            var args = CommandParser.ParseArguments<Args>(argsStr)
                .WithParsed(args => valid = Execute(args, out newInput));

            input = newInput;
            return valid;
        }

        private static bool Execute(Args args, out IPlayerInput? input)
        {
            input = null;

            var direction = CommandParser.ParseDirection(args.Direction);
            if (direction == null) return false;

            input = new MovementInput(direction, true);
            return true;
        }
    }
}