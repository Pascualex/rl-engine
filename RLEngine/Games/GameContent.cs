using RLEngine.Abilities;
using RLEngine.Boards;
using RLEngine.Entities;
using RLEngine.Utils;

namespace RLEngine.Games
{
    public class GameContent : IIdentifiable
    {
        public string ID { get; init; } = string.Empty;
        public Size BoardSize { get; init; } = Size.Zero;
        public TileType FloorType { get; init; } = new();
        public TileType WallType { get; init; } = new();
        public EntityType PlayerType { get; init; } = new();
        public EntityType GoblinType { get; init; } = new();
        public Ability Ability { get; init; } = new();
    }
}