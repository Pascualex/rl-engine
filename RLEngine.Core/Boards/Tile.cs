using RLEngine.Core.Entities;
using RLEngine.Core.Utils;

using System.Collections.Generic;

namespace RLEngine.Core.Boards
{
    internal class Tile
    {
        private readonly HashSet<IEntity> entities = new();

        public Tile(Coords position, TileType type)
        {
            Position = position;
            Type = type;
        }

        public Coords Position { get; }
        public IReadOnlyCollection<IEntity> Entities => entities;
        public TileType Type { get; private set; }

        public void Add(IEntity entity)
        {
            if (!CanAdd(entity)) throw new IncompatibleTileException(entity, Position);
            entities.Add(entity);
        }

        public void Remove(IEntity entity)
        {
            if (!entities.Contains(entity)) throw new SynchronizationException(entity, Position);
            entities.Remove(entity);
        }

        public void Modify(TileType type)
        {
            if (!CanModify(type)) throw new IncompatibleTileException(type, Position);
            Type = type;
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
            if (entity.IsGhost) return true;

            if (entity.BlocksGround && tileType.BlocksGround) return false;
            if (entity.BlocksAir && tileType.BlocksAir) return false;

            return true;
        }
    }
}