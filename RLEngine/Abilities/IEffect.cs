using RLEngine.Logs;
using RLEngine.State;
using RLEngine.Entities;

namespace RLEngine.Abilities
{
    public interface IEffect
    {
        Log? Cast(Entity caster, Entity target, GameState state);
    }
}