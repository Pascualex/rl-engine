using RLEngine.Abilities;
using RLEngine.Boards;
using RLEngine.Entities;

using System;

namespace RLEngine.Serialization.Yaml.Utils
{
    public static class SerializationPaths
    {
        public static string Get(Type type)
        {
            if (typeof(IAbility).IsAssignableFrom(type)) return "Abilities";
            if (typeof(ITileType).IsAssignableFrom(type)) return "TileTypes";
            if (typeof(IEntityType).IsAssignableFrom(type)) return "EntityTypes";
            throw new SerializationPathException(type);
        }
    }
}
