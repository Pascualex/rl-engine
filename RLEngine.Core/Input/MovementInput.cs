using RLEngine.Core.Utils;

namespace RLEngine.Core.Input
{
    public class MovementInput : IPlayerInput
    {
        public MovementInput(Coords to, bool isRelative)
        {
            To = to;
            IsRelative = isRelative;
        }

        public Coords To { get; }
        public bool IsRelative { get; }
    }
}