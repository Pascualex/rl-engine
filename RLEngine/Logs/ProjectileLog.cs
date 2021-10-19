using RLEngine.Entities;
using RLEngine.Utils;

namespace RLEngine.Logs
{
    public class ProjectileLog : Log
    {
        internal ProjectileLog(Coords from, Coords to)
        {
            From = from;
            To = to;
        }

        internal ProjectileLog(Coords from, Entity target)
        {
            From = from;
            Target = target;
        }

        internal ProjectileLog(Entity source, Coords to)
        {
            Source = source;
            To = to;
        }

        internal ProjectileLog(Entity source, Entity target)
        {
            Source = source;
            Target = target;
        }

        public Coords? From { get; } = null;
        public Coords? To { get; } = null;
        public Entity? Source { get; } = null;
        public Entity? Target { get; } = null;
    }
}