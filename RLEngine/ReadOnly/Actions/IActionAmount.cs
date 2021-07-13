using RLEngine.Entities;

namespace RLEngine.Actions
{
    public interface IActionAmount
    {
        int Calculate(Entity? entity);
    }
}