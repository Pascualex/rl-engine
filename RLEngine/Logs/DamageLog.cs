using RLEngine.Entities;

namespace RLEngine.Logs
{
    public class DamageLog : Log
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