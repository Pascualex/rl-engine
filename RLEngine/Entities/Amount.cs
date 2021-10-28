using System;

namespace RLEngine.Entities
{
    public class Amount
    {
        public int Base { get; init; } = 0;

        public int Calculate(IEntity target, IEntity? caster)
        {
            return Math.Max(0, Base);
        }
    }
}