using RLEngine.Core.Utils;

namespace RLEngine.Core.Entities
{
    public interface IEntity : IEntityAttributes, ITarget
    {
        bool IsActive { get; }
        bool IsPlayer { get; set; }
        int Health { get; }
        int MissingHealth { get; }
        EntityType Type { get; }

        int Damage(int damage);
        int Heal(int heal);

        void OnSpawn(Coords to);
        void OnMove(Coords to);
        void OnDestroy();
    }
}