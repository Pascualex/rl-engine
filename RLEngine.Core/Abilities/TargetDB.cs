using RLEngine.Core.Entities;
using RLEngine.Core.Utils;

using System;
using System.Collections.Generic;

namespace RLEngine.Core.Abilities
{
    internal class TargetDB
    {
        private readonly Dictionary<string, IEntity> entitiesDB = new();
        private readonly Dictionary<string, IReadOnlyCollection<IEntity>> entityGroupsDB = new();
        private readonly Dictionary<string, Coords> coordsDB = new();

        public void Add(string id, IEntity entity)
        {
            if (id.Length == 0) return;
            entitiesDB[id] = entity;
        }

        public void Add(string id, IReadOnlyCollection<IEntity> entityGroup)
        {
            if (id.Length == 0) return;
            entityGroupsDB[id] = entityGroup;
        }

        public void Add(string id, Coords coords)
        {
            if (id.Length == 0) return;
            coordsDB[id] = coords;
        }

        public IEntity? GetEntity(string id)
        {
            return TryGetEntity(id, out var entity) ? entity : null;
        }

        public IReadOnlyCollection<IEntity> GetEntityGroup(string id)
        {
            if (!entityGroupsDB.TryGetValue(id, out var entityGroup)) return Array.Empty<IEntity>();
            return entityGroup;
        }

        public Coords? GetCoords(string id)
        {
            return TryGetCoords(id, out var coords) ? coords : null;
        }

        public bool TryGetEntity(string id, out IEntity entity)
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
