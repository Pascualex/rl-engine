using RLEngine.Actions;
using RLEngine.Logs;
using RLEngine.State;

using NRE = System.NullReferenceException;

namespace RLEngine.Abilities
{
    public static class ProjectileEffect
    {
        internal static Log? Shoot(this IProjectileEffect effect,
        TargetDB targetDB, GameState state)
        {
            var from = targetDB.GetCoords(effect.Source);
            var to = targetDB.GetCoords(effect.Target);

            if (from != null && to != null)
            {
                return state.Shoot(from, to);
            }
            else if (from != null)
            {
                if (!targetDB.TryGetEntity(effect.Target, out var target)) throw new NRE();

                return state.Shoot(from, target);
            }
            else if (to != null)
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
