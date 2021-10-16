using RLEngine.Games;
using RLEngine.Input;

using System;

namespace RLEngine.Runner
{
    public static class InputManager
    {
        public static PlayerInput? GetInput(Game game)
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
                if (MovementCommand.Execute(command, args, out var mi)) return mi;
                if (AttackCommand.Execute(command, args, out var ai)) return ai;
                if (SpellCommand.Execute(command, args, game, out var si)) return si;

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" ## Invalid syntax");
                Console.ResetColor();
            }
        }
    }
}