using RLEngine.Core.Entities;
using RLEngine.Core.Utils;

using System;

namespace RLEngine.Core.Boards
{
    public class IncompatibleTileException : Exception
    {
        public IncompatibleTileException() : base()
        { }

        public IncompatibleTileException(string message) : base(message)
        { }

        public IncompatibleTileException(string message, Exception innerException)
        : base(message, innerException)
        { }

        public IncompatibleTileException(TileType tileType, Coords coords)
        : base($"The tile type {tileType} can not be applied to the tile at {coords}")
        { }

        public IncompatibleTileException(IEntity entity, Coords coords)
        : base($"The entity {entity} can not be added to the tile at {coords}")
        { }
    }
}