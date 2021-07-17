using RLEngine.Serialization.Boards;
using RLEngine.Serialization.Entities;
using RLEngine.Utils;

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

            for (var i = 0; i < 50; i++)
            {
                log = game.ProcessTurns();
                logger.Write(log);
            }
        }
    }
}