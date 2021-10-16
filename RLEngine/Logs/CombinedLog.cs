using System.Collections.Generic;

namespace RLEngine.Logs
{
    public class CombinedLog : Log
    {
        public IEnumerable<Log> Logs { get; }
        public bool IsParallel { get; }

        public CombinedLog(IEnumerable<Log> logs, bool isParallel)
        {
            Logs = logs;
            IsParallel = isParallel;
        }
    }
}
