using RLEngine.Entities;

namespace RLEngine.Input
{
    public class AttackInput : IPlayerInput
    {
        public AttackInput(IEntity target)
        {
            Target = target;
        }

        public IEntity Target { get; }
    }
}