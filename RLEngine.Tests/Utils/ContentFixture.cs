using RLEngine.Boards;
using RLEngine.Entities;

using NSubstitute;

namespace RLEngine.Tests.Utils
{
    public class ContentFixture
    {
        public IEntityType EntityType { get; }
        public IEntityType GroundEntityType { get; }
        public IEntityType AgentType { get; }
        public IEntityType SlowAgentType { get; }
        public IEntityType FastAgentType { get; }
        public IEntityType GhostAgentType { get; }
        public IEntityType UnparentedEntityType { get; }
        public int StandardActionCost { get; } = 100;
        public ITileType FloorTileType { get; }
        public ITileType WallTileType { get; }

        public ContentFixture()
        {
            EntityType = Substitute.For<IEntityType>();
            GroundEntityType = Substitute.For<IEntityType>();
            GroundEntityType.BlocksGround.Returns(true);
            GhostAgentType = Substitute.For<IEntityType>();
            GhostAgentType.IsAgent.Returns(true);
            GhostAgentType.BlocksGround.Returns(true);
            GhostAgentType.IsGhost.Returns(true);
            AgentType = Substitute.For<IEntityType>();
            AgentType.IsAgent.Returns(true);
            SlowAgentType = Substitute.For<IEntityType>();
            SlowAgentType.IsAgent.Returns(true);
            SlowAgentType.Speed.Returns(50);
            FastAgentType = Substitute.For<IEntityType>();
            FastAgentType.IsAgent.Returns(true);
            FastAgentType.Speed.Returns(150);
            UnparentedEntityType = Substitute.For<IEntityType>();
            UnparentedEntityType.Name.Returns("Unparented entity type");
            UnparentedEntityType.IsAgent.Returns(true);
            UnparentedEntityType.MaxHealth.Returns(80);
            UnparentedEntityType.Speed.Returns(120);
            UnparentedEntityType.BlocksGround.Returns(true);
            UnparentedEntityType.Parent.Returns((IEntityType?)null);
            FloorTileType = Substitute.For<ITileType>();
            WallTileType = Substitute.For<ITileType>();
            WallTileType.BlocksGround.Returns(true);
            WallTileType.BlocksAir.Returns(true);
        }
    }
}