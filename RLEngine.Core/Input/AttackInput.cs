using RLEngine.Core.Entities;

namespace RLEngine.Core.Input
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