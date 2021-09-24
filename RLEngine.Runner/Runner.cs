using RLEngine.Content.Abilities;
using RLEngine.Content.Boards;
using RLEngine.Content.Entities;

using RLEngine.Input;
using RLEngine.Abilities;
using RLEngine.Actions;
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
            var goblinType = new EntityType { Name = "Goblin", IsAgent = true };
            var ability = new Ability();
            ability.Add(new DamageEffect(new ActionAmount { Base = 20 }));
            ability.Add(new DamageEffect(new ActionAmount { Base =  5 }));
            var content = new GameContent
            (
                boardSize,
                floorType,
                wallType,
                playerType,
                goblinType,
                ability
            );

            var game = new Game(content);
            var logger = new Logger(250);

            var log = game.SetupExample();
            logger.Write(log);

            while (true)
            {
                var input = GetInput(game);
                if (input == null) break;
                game.Input = input;
                log = game.ProcessTurns();
                logger.Write(log);
            }
        }

        private static PlayerInput? GetInput(Game game)
        {
            while (true)
            {
                var valid = AskForInput(game, out var input);
                if (valid) return input;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" ## Invalid syntax");
                Console.ResetColor();
            }
        }

        private static bool AskForInput(Game game, out PlayerInput? input)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(" #> ");
            Console.ResetColor();
            const StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries;
            var words = Console.ReadLine().Trim().Split(new char[] { ' ' }, options);

            input = null;

            if (words.Length < 1) return false;
            var action = words[0];

            var ability = game.Content.Ability;
            if (action == "e" || action == "exit") return true;
            else if (action == "a" || action == "attack") input = new AttackInput();
            else if (action == "s" || action == "spell") input = new AbilityInput(ability);

            if (input != null) return true;

            if (words.Length < 2) return false;
            var dir = words[1];

            Coords coords = Coords.Zero;
            if      (dir == "u" ||    dir == "up") coords =    Coords.Up;
            else if (dir == "r" || dir == "right") coords = Coords.Right;
            else if (dir == "d" || dir ==  "down") coords =  Coords.Down;
            else if (dir == "l" || dir ==  "left") coords =  Coords.Left;

            if (coords == Coords.Zero) return false;

            if (action == "m" || action == "move") input = new MoveInput(coords, true);

            return input != null;
        }
    }
}