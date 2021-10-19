using RLEngine.Input;

using CommandLine;

namespace RLEngine.Runner
{
    public static class AttackCommand
    {
        private static readonly string[] aliases = { "attack", "a" };

        private class Args
        {
            [Value(0, Required = true)]
            public string Direction { get; set; } = string.Empty;
        }

        public static bool Execute(string command, string[] argsStr, out AttackInput? input)
        {
            input = null;
            if (!AliasesUtils.Accepts(command, aliases)) return false;

            var valid = false;
            AttackInput? newInput = null;
            var args = CommandParser.ParseArguments<Args>(argsStr)
                .WithParsed(args => valid = Execute(args, out newInput));

            input = newInput;
            return valid;
        }

        private static bool Execute(Args args, out AttackInput? input)
        {
            input = null;

            var direction = CommandParser.ParseDirection(args.Direction);
            if (direction is null) return false;

            // input = new AttackInput(direction);
            return true;
        }
    }
}