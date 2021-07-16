using RLEngine.Boards;
using RLEngine.Entities;

using NSubstitute;

namespace RLEngine.Tests.Utils
{
    public class ContentFixture
    {
        public IEntityType GroundEntityType { get; }
        public IEntityType GhostAgentType { get; }
        public IEntityType UnparentedEntityType { get; }
        public ITileType FloorTileType { get; }
        public ITileType WallTileType { get; }

        public ContentFixture()
        {
            GroundEntityType = Substitute.For<IEntityType>();
            GroundEntityType.BlocksGround.Returns(true);
            GhostAgentType = Substitute.For<IEntityType>();
            GhostAgentType.IsAgent.Returns(true);
            GhostAgentType.BlocksGround.Returns(true);
            GhostAgentType.IsGhost.Returns(true);
            UnparentedEntityType = Substitute.For<IEntityType>();
            UnparentedEntityType.Name.Returns("Unparented entity type");
            UnparentedEntityType.IsAgent.Returns(true);
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