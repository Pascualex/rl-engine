using RLEngine.Entities;
using RLEngine.Utils;

namespace RLEngine.Logs
{
    public class ProjectileLog : Log
    {
        public ProjectileLog(Coords from, Coords to)
        {
            From = from;
            To = to;
        }

        public ProjectileLog(Coords from, Entity target)
        {
            From = from;
            Target = target;
        }

        public ProjectileLog(Entity source, Coords to)
        {
            Source = source;
            To = to;
        }

        public ProjectileLog(Entity source, Entity target)
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