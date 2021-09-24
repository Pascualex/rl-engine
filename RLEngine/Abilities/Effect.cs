using RLEngine.Logs;
using RLEngine.State;
using RLEngine.Entities;

namespace RLEngine.Abilities
{
    public abstract class Effect
    {
        public abstract Log? Cast(Entity caster, Entity target, GameState state);
    }
}