using RLEngine.Core.Utils;

using System;

namespace RLEngine.Core.Entities
{
    internal class Entity : IEntity
    {
        public Entity(EntityType type)
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

        public bool IsActive { get; private set; } = false;
        public string Name { get; }
        public bool IsAgent { get; }
        public bool IsPlayer { get; private set; } = false;
        public int Health { get; private set; }
        public int MissingHealth => MaxHealth - Health;
        public int MaxHealth { get; }
        public int Speed { get; }
        public bool BlocksGround { get; }
        public bool BlocksAir { get; }
        public bool IsGhost { get; }
        public bool IsRoamer { get; }
        public EntityType Type { get; }
        public Coords Position { get; private set; } = Coords.MinusOne;

        int IEntity.Damage(int damage)
        {
            damage = damage.Clamp(0, Health);
            Health -= damage;
            return damage;
        }

        int IEntity.Heal(int heal)
        {
            heal = heal.Clamp(0, MissingHealth);
            Health += heal;
            return heal;
        }

        void IEntity.OnSpawn(Coords to)
        {
            IsActive = true;
            Position = to;
        }

        void IEntity.OnMove(Coords to)
        {
            Position = to;
        }

        void IEntity.OnDestroy()
        {
            IsActive = false;
        }

        void IEntity.OnControlChange(bool isPlayer)
        {
            IsPlayer = isPlayer;
        }
    }
}