using RLEngine.Core.Logs;
using RLEngine.Core.Abilities;
using RLEngine.Core.Entities;
using RLEngine.Core.Utils;

using System;

namespace RLEngine.Core.Actions
{
    internal partial class ActionExecutor
    {
        public AbilityLog? Cast(IEntity caster, Ability ability)
        {
            if (ability.Type != AbilityType.SelfTarget) throw new InvalidOperationException();
            if (!caster.IsActive) return null;
            return new(caster, ability);
        }

        public AbilityLog? Cast(IEntity caster, IEntity target, Ability ability)
        {
            if (ability.Type != AbilityType.EntityTarget) throw new InvalidOperationException();
            if (!caster.IsActive) return null;
            if (!target.IsActive) return null;
            return new(caster, ability, target);
        }

        public AbilityLog? Cast(IEntity caster, Coords target, Ability ability)
        {
            if (ability.Type != AbilityType.EntityTarget) throw new InvalidOperationException();
            if (!caster.IsActive) return null;
            return new(caster, ability, target);
        }
    }
}