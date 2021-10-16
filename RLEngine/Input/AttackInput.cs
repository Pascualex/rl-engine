using RLEngine.Utils;

namespace RLEngine.Input
{
    public class AttackInput : PlayerInput
    {
        public AttackInput(Coords coords)
        {
            Coords = coords;
        }

        public Coords Coords { get; }
    }
}