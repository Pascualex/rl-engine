using RLEngine.Boards;
using RLEngine.Entities;
using RLEngine.Utils;

namespace RLEngine
{
    public interface IGameContent
    {
        Size BoardSize { get; }
        ITileType FloorType { get; }
        ITileType WallType { get; }
        IEntityType PlayerType { get; }
        IEntityType GoblinType { get; }
    }
}