using RLEngine.Entities;

using System;

namespace RLEngine.Actions
{
    public class ActionAmount : IActionAmount
    {
        public int Base { get; set; } = 0;

        public int Calculate(Entity? entity)
        {
            return Math.Max(0, Base);
        }
    }
}