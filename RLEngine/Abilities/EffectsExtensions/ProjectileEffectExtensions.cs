using RLEngine.Actions;
using RLEngine.Logs;
using RLEngine.State;

using NRE = System.NullReferenceException;

namespace RLEngine.Abilities
{
    public static class ProjectileEffectExtensions
    {
        public static Log? Shoot(this IProjectileEffect effect,
        TargetDB targetDB, GameState state)
        {
            var from = targetDB.GetCoords(effect.From);
            var to = targetDB.GetCoords(effect.To);

            if (from is not null && to is not null)
            {
                return state.Shoot(from, to);
            }
            else if (from is not null)
            {
                if (!targetDB.TryGetEntity(effect.Target, out var target)) throw new NRE();

                return state.Shoot(from, target);
            }
            else if (to is not null)
            {
                if (!targetDB.TryGetEntity(effect.Source, out var source)) throw new NRE();

                return state.Shoot(source, to);
            }
            else
            {
                if (!targetDB.TryGetEntity(effect.Source, out var source)) throw new NRE();
                if (!targetDB.TryGetEntity(effect.Target, out var target)) throw new NRE();

                return state.Shoot(source, target);
            }
        }
    }
}
