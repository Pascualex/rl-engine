using RLEngine.Abilities;
using RLEngine.Entities;
using RLEngine.Utils;

namespace RLEngine.Logs
{
    public class AbilityLog : ILog
    {
        public AbilityLog(IEntity caster, Ability ability)
        : this(caster, ability, null) { }

        public AbilityLog(IEntity caster, Ability ability, ITarget? target)
        {
            Caster = caster;
            Ability = ability;
            Target = target;
        }

        public IEntity Caster { get; }
        public Ability Ability { get; }
        public ITarget? Target { get; }
    }
}
