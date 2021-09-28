using RLEngine.Abilities;
using RLEngine.Boards;
using RLEngine.Entities;
using RLEngine.Utils;

namespace RLEngine.Games
{
    public interface IGameContent : IIdentifiable
    {
        Size BoardSize { get; }
        ITileType FloorType { get; }
        ITileType WallType { get; }
        IEntityType PlayerType { get; }
        IEntityType GoblinType { get; }
        IAbility Ability { get; }
    }
}