using RLEngine.Boards;
using RLEngine.Utils;

namespace RLEngine.Logs
{
    public class ModifyLog : Log
    {
        public ModifyLog(ITileType newType, ITileType previousType, Coords at)
        {
            NewType = newType;
            PreviousType = previousType;
            At = at;
        }

        public ITileType NewType { get; }
        public ITileType PreviousType { get; }
        public Coords At { get; }
    }
}