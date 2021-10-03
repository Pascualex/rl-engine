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

        public bool TryGetEntity(string id, out Entity entity)
        {
            return entities.TryGetValue(id, out entity);
        }
    }
}
