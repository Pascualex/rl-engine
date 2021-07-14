using RLEngine.Entities;

namespace RLEngine.Logs
{
    public class DamageLog : Log
    {
        public DamageLog(IROEntity target, IROEntity? attacker, int damage, int actualDamage)
        {
            Target = target;
            Attacker = attacker;
            Damage = damage;
            ActualDamage = actualDamage;
        }

        public IROEntity Target { get; }
        public IROEntity? Attacker { get; }
        public int Damage { get; }
        public int ActualDamage { get; }
    }
}