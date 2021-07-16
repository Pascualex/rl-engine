namespace RLEngine.Entities
{
    public interface IEntityType : IEntityAttributes
    {
        IEntityType? Parent { get; }
    }
}