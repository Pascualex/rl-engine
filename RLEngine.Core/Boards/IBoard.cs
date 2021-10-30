using RLEngine.Core.Entities;
using RLEngine.Core.Utils;

using System.Collections.Generic;

namespace RLEngine.Core.Boards
{
    public interface IBoard
    {
        Size Size { get; }

        bool Add(IEntity entity, Coords at);
        bool Move(IEntity entity, Coords to);
        bool Remove(IEntity entity);
        bool Modify(TileType tileType, Coords at);
        bool CanAdd(IEntity entity, Coords at);
        bool CanMove(IEntity entity, Coords to);
        bool CanModify(TileType tileType, Coords at);
        IReadOnlyCollection<IEntity> GetEntities(Coords at);
        TileType? GetTileType(Coords at);
    }
}