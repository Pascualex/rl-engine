using RLEngine.Entities;

namespace RLEngine.Logs
{
    public class HealLog : Log
    {
        public HealLog(Entity target, Entity? healer, int healing, int actualHealing)
        {
            Target = target;
            Healer = healer;
            Healing = healing;
            ActualHealing = actualHealing;
        }

        public Entity Target { get; }
        public Entity? Healer { get; }
        public int Healing { get; }
        public int ActualHealing { get; }
    }
}
