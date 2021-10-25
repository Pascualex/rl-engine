using RLEngine.Utils;

using System;

namespace RLEngine.Entities
{
    public class Entity
    {
        internal Entity(EntityType type)
        {
            // TODO: support inheritance in types and overridden attributes
            Name = type.Name;
            IsAgent = type.IsAgent;
            Health = type.MaxHealth;
            MaxHealth = type.MaxHealth;
            Speed = type.Speed;
            BlocksGround = type.BlocksGround;
            BlocksAir = type.BlocksAir;
            IsGhost = type.IsGhost;
            IsRoamer = type.IsRoamer;
            Type = type;
        }

        public string Name { get; }
        public bool IsAgent { get; }
        public bool IsPlayer { get; internal set; } = false;
        internal int Health { get; private set; }
        internal int MissingHealth => MaxHealth - Health;
        internal int MaxHealth { get; }
        internal int Speed { get; }
        internal bool BlocksGround { get; }
        internal bool BlocksAir { get; }
        internal bool IsGhost { get; }
        internal bool IsRoamer { get; }
        public EntityType Type { get; }
        public Coords Position { get; private set; } = Coords.MinusOne;
        internal bool IsDestroyed { get; private set; } = false;

        internal int Damage(int damage)
        {
            damage = damage.Clamp(0, Health);
            Health -= damage;
            return damage;
        }

        internal int Heal(int heal)
        {
            heal = heal.Clamp(0, MissingHealth);
            Health += heal;
            return heal;
        }

        internal void OnMove(Coords to)
        {
            Position = to;
        }

        internal void OnDestroy()
        {
            IsDestroyed = true;
        }
    }
}