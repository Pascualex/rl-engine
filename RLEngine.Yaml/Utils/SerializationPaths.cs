using RLEngine.Abilities;
using RLEngine.Boards;
using RLEngine.Entities;

using System;

namespace RLEngine.Yaml.Utils
{
    public static class SerializationPaths
    {
        public static string Get(Type type)
        {
            if (typeof(Ability).IsAssignableFrom(type)) return "Abilities";
            if (typeof(TileType).IsAssignableFrom(type)) return "TileTypes";
            if (typeof(EntityType).IsAssignableFrom(type)) return "EntityTypes";
            throw new SerializationPathException(type);
        }
    }
}
