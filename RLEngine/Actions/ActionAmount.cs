using RLEngine.Entities;

using System;

namespace RLEngine.Actions
{
    public class ActionAmount
    {
        public int Base { get; init; } = 0;

        public int Calculate(Entity? entity)
        {
            return Math.Max(0, Base);
        }
    }
}
