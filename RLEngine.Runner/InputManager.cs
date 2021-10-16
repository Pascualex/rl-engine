using RLEngine.Games;
using RLEngine.Input;
using RLEngine.Utils;

using System;
using System.CommandLine;

namespace RLEngine.Runner
{
    public static class InputManager
    {
        private static readonly MainCommand mainCommand = new();

        public static PlayerInput? GetInput()
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(" #> ");
                Console.ResetColor();

                var args = Console.ReadLine();

                if (mainCommand.Execute(args, out var input)) return input;

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" ## Invalid syntax");
                Console.ResetColor();
            }
        }
    }
}