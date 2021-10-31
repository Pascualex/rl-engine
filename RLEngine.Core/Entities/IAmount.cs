using System;

namespace RLEngine.Core.Entities
{
    public interface IAmount
    {
        int Calculate(IEntity target, IEntity? caster);
    }
}