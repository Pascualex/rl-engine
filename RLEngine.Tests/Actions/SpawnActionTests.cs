using RLEngine.Actions;
using RLEngine.Logs;
using RLEngine.State;
using RLEngine.Utils;
using RLEngine.Tests.Utils;

using NUnit.Framework;

namespace RLEngine.Tests.Actions
{
    [TestFixture]
    public class SpawnActionTests
    {
        [Test]
        [TestCase(0, 0)]
        [TestCase(0, 1)]
        [TestCase(2, 2)]
        public void SpawnPasses(int x, int y)
        {
            var f = new ContentFixture();

            var state = new GameState(new Size(3, 3), f.FloorTileType);

            var position = new Coords(x, y);
            var log = state.Spawn(f.GroundEntityType, position, out var entity);
            var spawnLog = (SpawnLog)log.FailIfNull();
            Assert.That(spawnLog.Entity, Is.SameAs(entity));
            Assert.That(spawnLog.At, Is.EqualTo(position));

            entity = entity.FailIfNull();
            var entityPosition = state.Board.GetCoords(entity);
            Assert.That(entityPosition, Is.EqualTo(position));
        }

        [Test]
        [TestCase(0, -1)]
        [TestCase(-1, -1)]
        [TestCase(3, 0)]
        public void SpawnFailsOutOfBounds(int x, int y)
        {
            var f = new ContentFixture();

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
            var f = new ContentFixture();

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
        }

        [Test]
        public void SpawnFailsWithIncompatibleEntity()
        {
            var f = new ContentFixture();

            var state = new GameState(new Size(3, 3), f.FloorTileType);

            var position = new Coords(1, 1);
            state.Spawn(f.GroundEntityType, position, out var entityA);
            var log = state.Spawn(f.GroundEntityType, position, out var entityB);
            Assert.That(log, Is.Null);
            Assert.That(entityB, Is.Null);

            entityA = entityA.FailIfNull();
            var entityAPosition = state.Board.GetCoords(entityA);
            Assert.That(entityAPosition, Is.EqualTo(position));
        }

        [Test]
        public void SpawnFailsWithIncompatibleTile()
        {
            var f = new ContentFixture();

            var state = new GameState(new Size(3, 3), f.WallTileType);

            var position = new Coords(1, 1);
            var log = state.Spawn(f.GroundEntityType, position, out var entity);
            Assert.That(log, Is.Null);
            Assert.That(entity, Is.Null);
        }
    }
}