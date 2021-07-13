using RLEngine.Entities;

namespace RLEngine.Logs
{
    public class HealLog : Log
    {
        public HealLog(IEntity target, IEntity? healer, int heal, int actualHeal)
        {
            Target = target;
            Healer = healer;
            Heal = heal;
            ActualHeal = actualHeal;
        }

        public IEntity Target { get; }
        public IEntity? Healer { get; }
        public int Heal { get; }
        public int ActualHeal { get; }
    }
}