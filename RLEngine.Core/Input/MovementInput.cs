using RLEngine.Core.Utils;

namespace RLEngine.Core.Input
{
    public class MovementInput : IPlayerInput
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