using RLEngine.Entities;

using System;

namespace RLEngine.Events
{
    public class ActionAmount
    {
        public int Base { get; init; } = 0;

        internal int Calculate(Entity target, Entity? caster)
        {
            return Math.Max(0, Base);
        }
    }
}
