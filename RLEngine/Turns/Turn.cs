using RLEngine.Entities;

using System;

namespace RLEngine.Turns
{
    public class Turn : IComparable<Turn>
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
            var comparison = Tick.CompareTo(other.Tick);
            if (comparison != 0) return comparison;
            return -EntityId.CompareTo(other.EntityId);
        }
    }
}