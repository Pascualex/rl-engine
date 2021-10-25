using RLEngine.Games;
using RLEngine.Input;

using CommandLine;
using System.Linq;

namespace RLEngine.Runner
{
    public static class AbilityCommand
    {
        private static readonly string[] aliases = { "cast", "c" };

        private class Args
        {
            [Value(0, Required = true)]
            public string Direction { get; set; } = string.Empty;
        }

        public static bool Execute(string command, string[] argsStr, Game game,
        out IPlayerInput? input)
        {
            input = null;
            if (!AliasesUtils.Accepts(command, aliases)) return false;

            var valid = false;
            IPlayerInput? newInput = null;
            var args = CommandParser.ParseArguments<Args>(argsStr)
                .WithParsed(args => valid = Execute(args, game, out newInput));

            input = newInput;
            return valid;
        }

        private static bool Execute(Args args, Game game, out IPlayerInput? input)
        {
            input = null;

            var direction = CommandParser.ParseDirection(args.Direction);
            if (direction == null) return false;

            var currentAgent = game.CurrentAgent;
            if (currentAgent == null) return true;

            var position = currentAgent.Position + direction;
            var entities = game.Board.GetEntities(position);
            if (!entities.Any()) return true;

            input = new AbilityInput(game.Content.Ability, entities.First());
            return true;
        }
    }
}