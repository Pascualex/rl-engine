using System.Collections.Generic;

using RLEngine.Entities;

namespace RLEngine.Boards
{
    public interface ITile
    {
        IReadOnlyCollection<Entity> Entities { get; }
        ITileType Type { get; }
    }
}