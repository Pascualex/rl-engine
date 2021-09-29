using RLEngine.Serialization.Abilities;
using RLEngine.Serialization.Boards;
using RLEngine.Serialization.Entities;
using RLEngine.Serialization.Utils;

using RLEngine.Games;
using RLEngine.Abilities;
using RLEngine.Boards;
using RLEngine.Entities;
using RLEngine.Utils;

using YamlDotNet.Serialization;

namespace RLEngine.Serialization.Games
{
    public class GameContent : Deserializable, IGameContent
    {
        public Size BoardSize { get; set; } = Size.Zero;
        [YamlMember(SerializeAs = typeof(TileType))]
        public ITileType FloorType { get; set; } = new TileType();
        [YamlMember(SerializeAs = typeof(TileType))]
        public ITileType WallType { get; set; } = new TileType();
        [YamlMember(SerializeAs = typeof(EntityType))]
        public IEntityType PlayerType { get; set; } = new EntityType();
        [YamlMember(SerializeAs = typeof(EntityType))]
        public IEntityType GoblinType { get; set; } = new EntityType();
        [YamlMember(SerializeAs = typeof(Ability))]
        public IAbility Ability { get; set; } = new Ability();
    }
}