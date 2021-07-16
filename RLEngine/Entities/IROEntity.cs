namespace RLEngine.Entities
{
    public interface IROEntity : IEntityAttributes
    {
        IEntityType Type { get; }
    }
}