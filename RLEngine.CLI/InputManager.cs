using RLEngine.Core.Games;
using RLEngine.Core.Input;

using System;

namespace RLEngine.CLI
{
    public static class InputManager
    {
        public static IPlayerInput? GetInput(Game game)
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(" #> ");
                Console.ResetColor();

                const StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries;
                var fullArgs = Console.ReadLine().Trim().Split(new char[] { ' ' }, options);

                if (fullArgs.Length == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(" ## No input");
                    Console.ResetColor();
                    continue;
                }

                var command = fullArgs[0];
                var args = new string[fullArgs.Length - 1];
                Array.Copy(fullArgs, 1, args, 0, args.Length);

                if (ExitCommand.Execute(command)) return null;
                if (GetInput(command, args, game, out var input))
                {
                    if (input != null) return input;

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(" ## Invalid command");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(" ## Invalid syntax");
                    Console.ResetColor();
                }
            }
        }

        private static bool GetInput(string command, string[] args, Game game,
        out IPlayerInput? input)
        {
            if (MovementCommand.Execute(command, args, out input)) return true;
            if (AttackCommand.Execute(command, args, game, out input)) return true;
            if (AbilityCommand.Execute(command, args, game, out input)) return true;
            return false;
        }
    }
}