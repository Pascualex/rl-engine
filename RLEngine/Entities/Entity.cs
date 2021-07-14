using System;

namespace RLEngine.Entities
{
    public class Entity : IROEntity
    {
        public Entity(IEntityType type)
        {
            Type = type;
            Health = MaxHealth;
        }

        public string Name => Type.Name;
        public bool IsAgent => Type.IsAgent;
        public bool IsDead => Health <= 0;
        public int Health { get; private set; }
        public int MissingHealth => MaxHealth - Health;
        public int MaxHealth => Type.MaxHealth;
        public bool BlocksGround => Type.BlocksGround;
        public bool BlocksAir => Type.BlocksAir;
        public bool IsGhost => Type.IsGhost;
        public object? Visuals => Type.Visuals;
        public IEntityType Type { get; }

        public int Damage(int damage)
        {
            damage = Math.Clamp(damage, 0, Health);
            Health -= damage;
            return damage;
        }

        public int Heal(int heal)
        {
            heal = Math.Clamp(heal, 0, MissingHealth);
            Health += heal;
            return heal;
        }
    }
}