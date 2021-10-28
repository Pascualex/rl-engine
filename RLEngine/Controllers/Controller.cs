using RLEngine.Events;
using RLEngine.Logs;
using RLEngine.Entities;

namespace RLEngine.Controllers
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