using RLEngine.Serialization.Boards;
using RLEngine.Serialization.Entities;

using RLEngine.Boards;
using RLEngine.Entities;

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
            EntityType = new EntityType();
            GroundEntityType = new EntityType
            {
                BlocksGround = true,
            };
            GhostAgentType = new EntityType
            {
                IsAgent = true,
                BlocksGround = true,
                IsGhost = true,
            };
            AgentType = new EntityType
            {
                IsAgent = true,
            };
            SlowAgentType = new EntityType
            {
                IsAgent = true,
                Speed = 50,
            };
            FastAgentType = new EntityType
            {
                IsAgent = true,
                Speed = 150,
            };
            UnparentedEntityType = new EntityType
            {
                Name = "Unparented entity type",
                IsAgent = true,
                MaxHealth = 80,
                Speed = 120,
                BlocksGround = true,
                Parent = null,
            };
            FloorTileType = new TileType();
            WallTileType = new TileType
            {
                BlocksGround = true,
                BlocksAir = true,
            };
        }
    }
}