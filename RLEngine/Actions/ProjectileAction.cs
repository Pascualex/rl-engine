using RLEngine.Logs;
using RLEngine.State;
using RLEngine.Entities;
using RLEngine.Utils;

namespace RLEngine.Actions
{
    public static class ProjectileAction
    {
        public static Log? Shoot(this GameState state,
        Coords from, Coords to)
        {
            return new ProjectileLog(from, to);
        }

        public static Log? Shoot(this GameState state,
        Coords from, Entity target)
        {
            if (target.IsDestroyed) return null;

            return new ProjectileLog(from, target);
        }

        public static Log? Shoot(this GameState state,
        Entity source, Coords to)
        {
            if (source.IsDestroyed) return null;

            return new ProjectileLog(source, to);
        }

        public static Log? Shoot(this GameState state,
        Entity source, Entity target)
        {
            if (source.IsDestroyed) return null;
            if (target.IsDestroyed) return null;

            return new ProjectileLog(source, target);
        }
    }
}