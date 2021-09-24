using RLEngine.Actions;
using RLEngine.Logs;
using RLEngine.State;
using RLEngine.Entities;

namespace RLEngine.Abilities
{
    public class DamageEffect : Effect
    {
        public ActionAmount Amount { get; private init; }

        public DamageEffect(ActionAmount amount)
        {
            Amount = amount;
        }

        public override Log? Cast(Entity caster, Entity target, GameState state)
        {
            return state.Damage(target, caster, Amount);
        }
    }
}