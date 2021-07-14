using RLEngine.Boards;
using RLEngine.Entities;
using RLEngine.Utils;

namespace RLEngine
{
    public class GameContent : IGameContent
    {
        public GameContent(Size boardSize, ITileType floorType, ITileType wallTile, IEntityType playerType, IEntityType goblinType)
        {
            BoardSize = boardSize;
            FloorType = floorType;
            WallType = wallTile;
            PlayerType = playerType;
            GoblinType = goblinType;
        }

        public Size BoardSize { get; set; }
        public ITileType FloorType { get; set; }
        public ITileType WallType { get; set; }
        public IEntityType PlayerType { get; set; }
        public IEntityType GoblinType { get; set; }
    }
}