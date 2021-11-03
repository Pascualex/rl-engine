using RLEngine.Core.Utils;

namespace RLEngine.Core.Entities
{
    public interface IEntity : IEntityAttributes, ITarget
    {
        bool IsActive { get; }
        bool IsPlayer { get; }
        int Health { get; }
        int MissingHealth { get; }
        EntityType Type { get; }

        internal int Damage(int damage);
        internal int Heal(int heal);

        internal void OnSpawn(Coords to);
        internal void OnMove(Coords to);
        internal void OnDestroy();
        internal void OnControlChange(bool isPlayer);
    }
}