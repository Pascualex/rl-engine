using System.Collections.Generic;

namespace RLEngine.Logs
{
    public class CombinedLog : Log
    {
        private readonly IList<Log> logs = new List<Log>();

        public IEnumerable<Log> Logs => logs;
        public bool IsParallel { get; }

        public CombinedLog()
        : this(false) { }

        public CombinedLog(bool isParallel)
        {
            IsParallel = isParallel;
        }

        public void Add(Log? log)
        {
            if (log is not null) logs.Add(log);
        }
    }
}
