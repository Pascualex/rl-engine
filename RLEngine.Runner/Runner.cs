using RLEngine;
using RLEngine.Boards;
using RLEngine.Entities;
using RLEngine.Utils;

namespace RLEngine.Runner
{
    public static class Runner
    {
        public static void Main()
        {
            var boardSize = new Size(10, 10);
            var floorType = new TileType();
            var wallType = new TileType { BlocksGround = true, BlocksAir = true };
            var playerType = new EntityType { Name = "Goblin" };
            var content = new GameContent(boardSize, floorType, wallType, playerType);

            var game = new Game(content);
            var log = game.SetupExample();

            var logger = new Logger(250);
            logger.Write(log);
        }
    }
}