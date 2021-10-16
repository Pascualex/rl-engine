using RLEngine.Utils;

namespace RLEngine.Input
{
    public class MovementInput : PlayerInput
    {
        public MovementInput(Coords to, bool relative)
        {
            To = to;
            Relative = relative;
        }

        public Coords To { get; }
        public bool Relative { get; }
    }
}