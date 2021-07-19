using RLEngine.Input;
using RLEngine.Serialization.Boards;
using RLEngine.Serialization.Entities;
using RLEngine.Utils;

using System;

namespace RLEngine.Runner
{
    public static class Runner
    {
        public static void Main()
        {
            var boardSize = new Size(10, 10);
            var floorType = new TileType() { Name = "Floor" };
            var wallType = new TileType { Name = "Wall", BlocksGround = true, BlocksAir = true };
            var playerType = new EntityType { Name = "Pascu", IsAgent = true };
            var goblinType = new EntityType { Name = "Echenike", IsAgent = true };
            var content = new GameContent(boardSize, floorType, wallType, playerType, goblinType);

            var game = new Game(content);
            var logger = new Logger(250);

            var log = game.SetupExample();
            logger.Write(log);

            while (true)
            {
                var input = GetInput();
                if (input == null) break;
                game.Input = input;
                log = game.ProcessTurns();
                logger.Write(log);
            }
        }

        private static PlayerInput? GetInput()
        {
            while (true)
            {
                var valid = AskForInput(out var input);
                if (valid) return input;
            }
        }

        private static bool AskForInput(out PlayerInput? input)
        {
            Console.Write(" #> ");
            var str = Console.ReadLine().Trim();
            input = null;

            if (str == "exit") return true;
            else if (str ==    "up") input = new MoveInput(   Coords.Up, true);
            else if (str == "right") input = new MoveInput(Coords.Right, true);
            else if (str ==  "down") input = new MoveInput( Coords.Down, true);
            else if (str ==  "left") input = new MoveInput( Coords.Left, true);

            return input != null;
        }
    }
}