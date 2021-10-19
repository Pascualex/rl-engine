using System.Collections.Generic;

namespace RLEngine.Logs
{
    internal class CombinedLogBuilder
    {
        private readonly IList<Log> logs = new List<Log>();
        private readonly bool isParallel;

        public CombinedLogBuilder(bool isParallel)
        {
            this.isParallel = isParallel;
        }

        public void Add(Log? log)
        {
            if (log is null) return;
            logs.Add(log);
        }

        public CombinedLog? Build()
        {
            if (logs.Count == 0) return null;
            return new CombinedLog(logs, isParallel);
        }

        public CombinedLog ForceBuild()
        {
            return new CombinedLog(logs, isParallel);
        }
    }
}
