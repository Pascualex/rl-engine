using RLEngine.Entities;

using System.Collections.Generic;

namespace RLEngine.Boards
{
    public class Tile
    {
        private readonly HashSet<Entity> entities = new();

        public Tile(TileType type)
        {
            Type = type;
        }

        public IEnumerable<Entity> Entities => entities;
        public TileType Type { get; private set; }

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

        public bool Modify(TileType type)
        {
            if (!CanModify(type)) return false;
            Type = type;
            return true;
        }

        public bool CanAdd(Entity entity)
        {
            if (entities.Contains(entity)) return false;
            foreach (var other in entities)
            {
                if (!AreCompatible(entity, other)) return false;
            }
            return AreCompatible(entity, Type);
        }

        public bool CanModify(TileType type)
        {
            if (type == Type) return true;
            foreach (var entity in entities)
            {
                if (!AreCompatible(entity, type)) return false;
            }
            return true;
        }

        private static bool AreCompatible(Entity entityA, Entity entityB)
        {
            var shareIsAgent = entityA.IsAgent == entityB.IsAgent;
            var shareIsGhost = entityA.IsGhost == entityB.IsGhost;
            if (!shareIsAgent && !shareIsGhost) return true;

            if (entityA.BlocksGround && entityB.BlocksGround) return false;
            if (entityA.BlocksAir && entityB.BlocksAir) return false;

            return true;
        }

        private static bool AreCompatible(Entity entity, TileType tileType)
        {
            if (entity.Type.IsGhost) return true;

            if (entity.BlocksGround && tileType.BlocksGround) return false;
            if (entity.BlocksAir && tileType.BlocksAir) return false;

            return true;
        }
    }
}