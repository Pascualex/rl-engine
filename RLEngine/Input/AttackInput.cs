using RLEngine.Entities;

namespace RLEngine.Input
{
    public class AttackInput : IPlayerInput
    {
        public AttackInput(Entity target)
        {
            Target = target;
        }

        public Entity Target { get; }
    }
}