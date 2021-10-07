using RLEngine.Entities;

using System.Collections.Generic;

namespace RLEngine.Abilities
{
    public class TargetDB
    {
        private readonly Dictionary<string, Entity> entities = new();

        public TargetDB(Entity caster, Entity target)
        : this(caster)
        {
            entities.Add("target", target);
        }

        public TargetDB(Entity caster)
        {
            entities.Add("caster", caster);
        }

        public void Add(string id, Entity entity)
        {
            if (id.Length == 0) return;
            entities.Add(id, entity);
        }

        public Entity? GetEntity(string id)
        {
            return TryGetEntity(id, out var entity) ? entity : null;
        }

        public bool TryGetEntity(string id, out Entity entity)
        {
            entity = null!;
            if (id.Length == 0) return false;
            return entities.TryGetValue(id, out entity);
        }
    }
}
