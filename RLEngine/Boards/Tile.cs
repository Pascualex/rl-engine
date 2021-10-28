using RLEngine.Entities;

using System.Collections.Generic;

namespace RLEngine.Boards
{
    internal class Tile
    {
        private readonly HashSet<IEntity> entities = new();

        public Tile(TileType type)
        {
            Type = type;
        }

        public IReadOnlyCollection<IEntity> Entities => entities;
        public TileType Type { get; private set; }

        public bool Add(IEntity entity)
        {
            if (!CanAdd(entity)) return false;
            entities.Add(entity);
            return true;
        }

        public void Remove(IEntity entity)
        {
            entities.Remove(entity);
        }

        public bool Modify(TileType type)
        {
            if (!CanModify(type)) return false;
            Type = type;
            return true;
        }

        public bool CanAdd(IEntity entity)
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

        private static bool AreCompatible(IEntity entityA, IEntity entityB)
        {
            var shareIsAgent = entityA.IsAgent == entityB.IsAgent;
            var shareIsGhost = entityA.IsGhost == entityB.IsGhost;
            if (!shareIsAgent && !shareIsGhost) return true;

            if (entityA.BlocksGround && entityB.BlocksGround) return false;
            if (entityA.BlocksAir && entityB.BlocksAir) return false;

            return true;
        }

        private static bool AreCompatible(IEntity entity, TileType tileType)
        {
            if (entity.Type.IsGhost) return true;

            if (entity.BlocksGround && tileType.BlocksGround) return false;
            if (entity.BlocksAir && tileType.BlocksAir) return false;

            return true;
        }
    }
}