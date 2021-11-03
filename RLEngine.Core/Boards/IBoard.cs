using RLEngine.Core.Entities;
using RLEngine.Core.Utils;

namespace RLEngine.Core.Boards
{
    public interface IBoard : IReadOnlyBoard
    {
        void Add(IEntity entity, Coords at);
        void Move(IEntity entity, Coords to);
        void Remove(IEntity entity);
        void Modify(TileType tileType, Coords at);
    }
}