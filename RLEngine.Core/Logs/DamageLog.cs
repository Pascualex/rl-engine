using RLEngine.Core.Entities;

namespace RLEngine.Core.Logs
{
    public class DamageLog : ILog
    {
        public DamageLog(IEntity target, IEntity? attacker, int damage, int actualDamage)
        {
            Target = target;
            Attacker = attacker;
            Damage = damage;
            ActualDamage = actualDamage;
        }

        public IEntity Target { get; }
        public IEntity? Attacker { get; }
        public int Damage { get; }
        public int ActualDamage { get; }
    }
}
