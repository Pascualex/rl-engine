using RLEngine.Entities;

namespace RLEngine.Logs
{
    public class HealLog : Log
    {
        public HealLog(IROEntity target, IROEntity? healer, int heal, int actualHeal)
        {
            Target = target;
            Healer = healer;
            Heal = heal;
            ActualHeal = actualHeal;
        }

        public IROEntity Target { get; }
        public IROEntity? Healer { get; }
        public int Heal { get; }
        public int ActualHeal { get; }
    }
}