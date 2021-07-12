using RLEngine.Entities;

using System.Collections.Generic;

namespace RLEngine.Boards
{
    public class Tile : ITile
    {
        private readonly HashSet<Entity> entities = new();

        public Tile(ITileType type)
        {
            Type = type;
        }

        public IReadOnlyCollection<Entity> Entities => entities;
        public ITileType Type { get; private set; }

        public bool Add(Entity entity)
        {
            if (!CanAdd(entity)) return false;
            entities.Add(entity);
            return true;
        }

        public void Remove(Entity entity)
        {
            entities.Remove(entity);
        }

        public bool ChangeType(ITileType type)
        {
            if (!CanChangeType(type)) return false;
            Type = type;
            return true;
        }

        public bool CanAdd(Entity entity)
        {
            if (entities.Contains(entity)) return false;

            foreach (var other in entities)
                if (!AreCompatible(entity, other)) return false;
            if (!AreCompatible(entity, Type)) return false;

            return true;
        }

        public bool CanChangeType(ITileType type)
        {
            foreach (var entity in entities)
                if (!AreCompatible(entity, type)) return false;

            return true;
        }

        private static bool AreCompatible(Entity entityA, Entity entityB)
        {
            var shareIsAgent = entityA.Type.IsAgent == entityB.Type.IsAgent;
            var shareIsGhost = entityA.Type.IsGhost == entityB.Type.IsGhost;
            if (!shareIsAgent && !shareIsGhost) return true;

            if (entityA.Type.BlocksGround && entityB.Type.BlocksGround) return false;
            if (entityA.Type.BlocksAir && entityB.Type.BlocksAir) return false;

            return true;
        }

        private static bool AreCompatible(Entity entity, ITileType tileType)
        {
            if (entity.Type.IsGhost) return true;

            if (entity.Type.BlocksGround && tileType.BlocksGround) return false;
            if (entity.Type.BlocksAir && tileType.BlocksAir) return false;

            return true;
        }
    }
}