using RLEngine.Events;
using RLEngine.Logs;
using RLEngine.Entities;

namespace RLEngine.Controllers
{
    internal interface IController
    {
        bool TryProcessTurn(Entity entity, EventContext ctx, out Log? log);
    }
}