using RLEngine.Utils;

namespace RLEngine.Entities
{
    public interface IEntityType : IIdentifiable, IEntityAttributes
    {
        IEntityType? Parent { get; }
    }
}