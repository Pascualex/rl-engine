using RLEngine.Entities;

namespace RLEngine.Logs
{
    public class DamageLog : Log
    {
        public DamageLog(Entity target, Entity? attacker, int damage, int actualDamage)
        {
            Target = target;
            Attacker = attacker;
            Damage = damage;
            ActualDamage = actualDamage;
        }

        public Entity Target { get; }
        public Entity? Attacker { get; }
        public int Damage { get; }
        public int ActualDamage { get; }
    }
}
