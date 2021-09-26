using RLEngine.Abilities;
using RLEngine.Boards;
using RLEngine.Entities;
using RLEngine.Utils;

namespace RLEngine.Serialization
{
    public class GameContent : IGameContent
    {
        public GameContent(Size boardSize, ITileType floorType, ITileType wallTile, IEntityType playerType, IEntityType goblinType, IAbility ability)
        {
            BoardSize = boardSize;
            FloorType = floorType;
            WallType = wallTile;
            PlayerType = playerType;
            GoblinType = goblinType;
            Ability = ability;
        }

        public Size BoardSize { get; set; }
        public ITileType FloorType { get; set; }
        public ITileType WallType { get; set; }
        public IEntityType PlayerType { get; set; }
        public IEntityType GoblinType { get; set; }
        public IAbility Ability { get; set; }
    }
}