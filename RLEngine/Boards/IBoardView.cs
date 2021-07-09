using RLEngine.Entities;
using RLEngine.Utils;

namespace  RLEngine.Boards
{
    public interface IBoardView
    {
        void Add(Entity entity, Coords at);
        void Move(Entity entity, Coords to);
        void Remove(Entity entity);
        void Modify(Coords at, ITileType tileType);
    }
}