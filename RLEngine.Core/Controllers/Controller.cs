using RLEngine.Core.Events;
using RLEngine.Core.Logs;
using RLEngine.Core.Entities;

namespace RLEngine.Core.Controllers
{
    internal abstract class Controller
    {
        protected EventContext ctx;

        protected Controller(EventContext ctx)
        {
            this.ctx = ctx;
        }

        public abstract bool TryProcessTurn(IEntity entity, out ILog? log);
    }
}