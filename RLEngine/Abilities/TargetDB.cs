using RLEngine.Entities;
using RLEngine.Utils;

using System.Collections.Generic;

namespace RLEngine.Abilities
{
    public class TargetDB
    {
        private readonly Dictionary<string, Entity> entitiesDB = new();
        private readonly Dictionary<string, Coords> coordsDB = new();

        public TargetDB(Entity caster, Entity target)
        : this(caster)
        {
            entitiesDB.Add("target", target);
        }

        public TargetDB(Entity caster)
        {
            entitiesDB.Add("caster", caster);
        }

        public void Add(string id, Entity entity)
        {
            if (id.Length == 0) return;
            entitiesDB.Add(id, entity);
        }

        public void Add(string id, Coords coords)
        {
            if (id.Length == 0) return;
            coordsDB.Add(id, coords);
        }

        public Entity? GetEntity(string id)
        {
            return TryGetEntity(id, out var entity) ? entity : null;
        }

        public Coords? GetCoords(string id)
        {
            return TryGetCoords(id, out var coords) ? coords : null;
        }

        public bool TryGetEntity(string id, out Entity entity)
        {
            entity = null!;
            if (id.Length == 0) return false;
            return entitiesDB.TryGetValue(id, out entity);
        }

        public bool TryGetCoords(string id, out Coords coords)
        {
            coords = null!;
            if (id.Length == 0) return false;
            return coordsDB.TryGetValue(id, out coords);
        }
    }
}
