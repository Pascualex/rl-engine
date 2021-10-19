using RLEngine.Logs;
using RLEngine.State;
using RLEngine.Entities;

namespace RLEngine.Controllers
{
    internal interface IController
    {
        bool ProcessTurn(Entity entity, GameState state, out Log? log);
    }
}