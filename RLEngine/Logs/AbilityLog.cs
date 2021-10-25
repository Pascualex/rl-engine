using RLEngine.Abilities;
using RLEngine.Entities;

namespace RLEngine.Logs
{
    public class AbilityLog : Log
    {
        internal AbilityLog(Entity caster, Ability ability, ITarget target)
        {
            Caster = caster;
            Ability = ability;
            Target = target;
        }

        public Entity Caster { get; }
        public Ability Ability { get; }
        public ITarget Target { get; }
    }
}
