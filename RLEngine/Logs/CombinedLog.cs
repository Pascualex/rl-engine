using System.Collections.Generic;

namespace RLEngine.Logs
{
    public class CombinedLog : Log
    {
        private readonly IList<Log> logs = new List<Log>();

        public IEnumerable<Log> Logs => logs;

        public void Add(Log? log)
        {
            if (log is not null) logs.Add(log);
        }
    }
}
