using RLEngine.Utils;

namespace RLEngine.Entities
{
    public interface IEntity : IEntityAttributes, ITarget
    {
        bool IsPlayer { get; set; }
        int Health { get; }
        int MissingHealth { get; }
        EntityType Type { get; }
        bool IsDestroyed { get; }

        int Damage(int damage);
        int Heal(int heal);

        void OnMove(Coords to);
        void OnDestroy();
    }
}