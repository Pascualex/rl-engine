using RLEngine.Utils;

namespace RLEngine.Logs
{
    public class CoordsTarget : ITarget
    {
        public Coords Coords { get; }

        public CoordsTarget(Coords coords)
        {
            Coords = coords;
        }
    }
}