using RLEngine.Entities;

using System;

namespace RLEngine.Turns
{
    internal class Turn : IComparable<Turn>
    {
        public int Tick { get; }
        public int EntityId { get; }
        public Entity Entity { get; }

        public Turn(int tick, int entityId, Entity entity)
        {
            Tick = tick;
            EntityId = entityId;
            Entity = entity;
        }

        public Turn NewAfterTicks(int ticks)
        {
            return new Turn(Tick + ticks, EntityId, Entity);
        }

        public int CompareTo(Turn other)
        {
            var tickComparison = Tick.CompareTo(other.Tick);
            if (tickComparison != 0) return tickComparison;
            var speedComparison = Entity.Speed.CompareTo(other.Entity.Speed);
            if (speedComparison != 0) return -speedComparison;
            return EntityId.CompareTo(other.EntityId);
        }
    }
}