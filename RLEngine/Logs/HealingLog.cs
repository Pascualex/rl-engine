using RLEngine.Entities;

namespace RLEngine.Logs
{
    public class HealingLog : ILog
    {
        public HealingLog(IEntity target, IEntity? healer, int healing, int actualHealing)
        {
            Target = target;
            Healer = healer;
            Healing = healing;
            ActualHealing = actualHealing;
        }

        public IEntity Target { get; }
        public IEntity? Healer { get; }
        public int Healing { get; }
        public int ActualHealing { get; }
    }
}
