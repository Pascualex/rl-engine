using RLEngine.Entities;

namespace RLEngine.ViewState.Entities
{
    public class EntityViewState
    {
        internal EntityViewState(Entity reference)
        {
            Reference = reference;
        }

        public Entity Reference { get; }
    }
}