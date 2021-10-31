using System;

namespace RLEngine.Core.Entities
{
    public class Amount : IAmount
    {
        public int Base { get; init; } = 0;

        public int Calculate(IEntity target, IEntity? caster)
        {
            return Math.Max(0, Base);
        }
    }
}