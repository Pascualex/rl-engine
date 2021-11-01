using RLEngine.Core.Abilities;
using RLEngine.Core.Boards;
using RLEngine.Core.Entities;

using System;

namespace RLEngine.Yaml.Utils
{
    internal static class SerializationPaths
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
