using NUnit.Framework;
using NSubstitute;

using RLEngine.Actions;
using RLEngine.Logs;
using RLEngine.State;
using RLEngine.Boards;
using RLEngine.Entities;
using RLEngine.Utils;
using RLEngine.Tests.Utils;

namespace RLEngine.Tests.Actions
{
    [TestFixture]
    public class SpawnActionTests
    {
        private class Fixture
        {
            public IEntityType GroundEntityType { get; }
            public IEntityType GhostAgentType { get; }
            public ITileType FloorTileType { get; }
            public ITileType WallTileType { get; }

            public Fixture()
            {
                GroundEntityType = Substitute.For<IEntityType>();
                GroundEntityType.BlocksGround.Returns(true);
                GhostAgentType = Substitute.For<IEntityType>();
                GhostAgentType.IsAgent.Returns(true);
                GhostAgentType.BlocksGround.Returns(true);
                GhostAgentType.IsGhost.Returns(true);
                FloorTileType = Substitute.For<ITileType>();
                WallTileType = Substitute.For<ITileType>();
                WallTileType.BlocksGround.Returns(true);
                WallTileType.BlocksAir.Returns(true);
            }
        }

        [Test]
        [TestCase(0, 0)]
        [TestCase(0, 1)]
        [TestCase(2, 2)]
        public void SpawnPasses(int x, int y)
        {
            var f = new Fixture();

            var state = new GameState(new Size(3, 3), f.FloorTileType);

            var position = new Coords(x, y);
            var log = state.Spawn(f.GroundEntityType, position, out var entity);
            var spawnLog = (SpawnLog)log.FailIfNull();
            Assert.That(spawnLog.Entity, Is.SameAs(entity));
            Assert.That(spawnLog.At, Is.EqualTo(position));

            entity = entity.FailIfNull();
            var entityPosition = state.Board.GetCoords(entity);
            Assert.That(entityPosition, Is.EqualTo(position));

            var entities = state.Board.GetEntities(position);
            Assert.That(entities, Has.Member(entity));
        }

        [Test]
        [TestCase(0, -1)]
        [TestCase(-1, -1)]
        [TestCase(3, 0)]
        public void SpawnFailsOutOfBounds(int x, int y)
        {
            var f = new Fixture();

            var state = new GameState(new Size(3, 3), f.FloorTileType);

            var position = new Coords(x, y);
            var log = state.Spawn(f.GroundEntityType, position, out var entity);
            Assert.That(log, Is.Null);
            Assert.That(entity, Is.Null);

            var entities = state.Board.GetEntities(position);
            Assert.That(entities, Is.Empty);
        }

        [Test]
        public void SpawnPassesWithCompatibleEntity()
        {
            var f = new Fixture();

            var state = new GameState(new Size(3, 3), f.FloorTileType);

            var position = new Coords(1, 1);
            state.Spawn(f.GroundEntityType, position, out var entityA);
            var log = state.Spawn(f.GhostAgentType, position, out var entityB);
            var spawnLog = (SpawnLog)log.FailIfNull();
            Assert.That(spawnLog.Entity, Is.SameAs(entityB));
            Assert.That(spawnLog.At, Is.EqualTo(position));

            entityA = entityA.FailIfNull();
            var entityAPosition = state.Board.GetCoords(entityA);
            Assert.That(entityAPosition, Is.EqualTo(position));

            entityB = entityB.FailIfNull();
            var entityBPosition = state.Board.GetCoords(entityB);
            Assert.That(entityBPosition, Is.EqualTo(position));

            var entities = state.Board.GetEntities(position);
            Assert.That(entities, Has.Member(entityA));
            Assert.That(entities, Has.Member(entityB));
        }

        [Test]
        public void SpawnFailsWithIncompatibleEntity()
        {
            var f = new Fixture();

            var state = new GameState(new Size(3, 3), f.FloorTileType);

            var position = new Coords(1, 1);
            state.Spawn(f.GroundEntityType, position, out var entityA);
            var log = state.Spawn(f.GroundEntityType, position, out var entityB);
            Assert.That(log, Is.Null);
            Assert.That(entityB, Is.Null);

            entityA = entityA.FailIfNull();
            var entityAPosition = state.Board.GetCoords(entityA);
            Assert.That(entityAPosition, Is.EqualTo(position));

            var entities = state.Board.GetEntities(position);
            Assert.That(entities, Is.All.SameAs(entityA));
        }

        [Test]
        public void SpawnFailsWithIncompatibleTile()
        {
            var f = new Fixture();

            var state = new GameState(new Size(3, 3), f.WallTileType);

            var position = new Coords(1, 1);
            var log = state.Spawn(f.GroundEntityType, position, out var entity);
            Assert.That(log, Is.Null);
            Assert.That(entity, Is.Null);

            var entities = state.Board.GetEntities(position);
            Assert.That(entities, Is.Empty);
        }
    }
}