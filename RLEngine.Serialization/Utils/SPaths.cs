using RLEngine.Abilities;
using RLEngine.Boards;
using RLEngine.Entities;

using System;

namespace RLEngine.Serialization.Utils
{
    public static class SPaths
    {
        public static string Get(Type type)
        {
            if (type == typeof(IAbility)) return "Abilities";
            if (type == typeof(ITileType)) return "TileTypes";
            if (type == typeof(IEntityType)) return "EntityTypes";
            throw new UnsupportedTypeException(type);
        }
    }
}
