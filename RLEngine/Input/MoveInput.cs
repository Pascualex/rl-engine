using RLEngine.Utils;

namespace RLEngine.Input
{
    public class MoveInput : PlayerInput
    {
        public Coords To { get; }
        public bool Relative { get; }

        public MoveInput(Coords to, bool relative)
        {
            To = to;
            Relative = relative;
        }
    }
}