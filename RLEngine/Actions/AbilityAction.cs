using RLEngine.Logs;
using RLEngine.Abilities;
using RLEngine.Entities;
using RLEngine.Utils;

using System;

namespace RLEngine.Actions
{
    internal partial class ActionExecutor
    {
        public AbilityLog? Cast(IEntity caster, Ability ability)
        {
            if (ability.Type != AbilityType.SelfTarget) throw new InvalidOperationException();
            if (caster.IsDestroyed) return null;
            return new(caster, ability);
        }

        public AbilityLog? Cast(IEntity caster, IEntity target, Ability ability)
        {
            if (ability.Type != AbilityType.EntityTarget) throw new InvalidOperationException();
            if (caster.IsDestroyed) return null;
            if (target.IsDestroyed) return null;
            return new(caster, ability, target);
        }

        public AbilityLog? Cast(IEntity caster, Coords target, Ability ability)
        {
            if (ability.Type != AbilityType.EntityTarget) throw new InvalidOperationException();
            if (caster.IsDestroyed) return null;
            return new(caster, ability, target);
        }
    }
}