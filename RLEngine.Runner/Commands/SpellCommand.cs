using RLEngine.Games;
using RLEngine.Input;

using CommandLine;

namespace RLEngine.Runner
{
    public static class SpellCommand
    {
        private static readonly string[] aliases = { "cast", "c" };

        private class Args
        {
            [Value(0, Required = true)]
            public string Direction { get; set; } = string.Empty;
        }

        public static bool Execute(string command, string[] argsStr, Game game,
        out SpellInput? input)
        {
            input = null;
            if (!AliasesUtils.Accepts(command, aliases)) return false;

            var valid = false;
            SpellInput? newInput = null;
            var args = CommandParser.ParseArguments<Args>(argsStr)
                .WithParsed(args => valid = Execute(args, game, out newInput));

            input = newInput;
            return valid;
        }

        private static bool Execute(Args args, Game game, out SpellInput? input)
        {
            input = null;

            var direction = CommandParser.ParseDirection(args.Direction);
            if (direction is null) return false;

            // input = new SpellInput(game.Content.Ability, direction);
            return true;
        }
    }
}