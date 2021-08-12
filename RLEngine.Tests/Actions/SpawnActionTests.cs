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
            // Arrange
            var f = new ContentFixture();
            var state = new GameState(new Size(3, 3), f.FloorTileType);
            var position = new Coords(x, y);

            // Act
            var log = state.Spawn(f.AgentType, position, out var entity);

            // Assert
            var spawnLog = (SpawnLog)log.FailIfNull();
            Assert.That(spawnLog.Entity, Is.SameAs(entity));
            Assert.That(spawnLog.At, Is.EqualTo(position));
            entity = entity.FailIfNull();
            var currentEntity = state.TurnManager.Current;
            Assert.That(currentEntity, Is.SameAs(entity));
            var found = state.Board.TryGetCoords(entity, out var entityPosition);
            Assert.That(found, Is.True);
            Assert.That(entityPosition, Is.EqualTo(position));
        }

        [Test]
        [TestCase(0, -1)]
        [TestCase(-1, -1)]
        [TestCase(3, 0)]
        public void SpawnFailsOutOfBounds(int x, int y)
        {
            // Arrange
            var f = new ContentFixture();
            var state = new GameState(new Size(3, 3), f.FloorTileType);
            var position = new Coords(x, y);

            // Act
            var log = state.Spawn(f.GroundEntityType, position, out var entity);

            // Assert
            Assert.That(log, Is.Null);
            Assert.That(entity, Is.Null);
            var currentEntity = state.TurnManager.Current;
            Assert.That(currentEntity, Is.Null);
            var entities = state.Board.GetEntities(position);
            Assert.That(entities, Is.Empty);
        }

        [Test]
        public void SpawnPassesWithCompatibleEntity()
        {
            // Arrange
            var f = new ContentFixture();
            var state = new GameState(new Size(3, 3), f.FloorTileType);
            var position = new Coords(1, 1);
            state.Spawn(f.GroundEntityType, position, out var entityA);
            entityA = entityA.FailIfNull();

            // Act
            var log = state.Spawn(f.GhostAgentType, position, out var entityB);

            // Assert
            var spawnLog = (SpawnLog)log.FailIfNull();
            Assert.That(spawnLog.Entity, Is.SameAs(entityB));
            Assert.That(spawnLog.At, Is.EqualTo(position));
            var entityAFound = state.Board.TryGetCoords(entityA, out var entityAPosition);
            Assert.That(entityAFound, Is.True);
            Assert.That(entityAPosition, Is.EqualTo(position));
            entityB = entityB.FailIfNull();
            var entityBFound = state.Board.TryGetCoords(entityB, out var entityBPosition);
            Assert.That(entityBFound, Is.True);
            Assert.That(entityBPosition, Is.EqualTo(position));
        }

        [Test]
        public void SpawnFailsWithIncompatibleEntity()
        {
            // Arrange
            var f = new ContentFixture();
            var state = new GameState(new Size(3, 3), f.FloorTileType);
            var position = new Coords(1, 1);
            state.Spawn(f.GroundEntityType, position, out var entityA);
            entityA = entityA.FailIfNull();

            // Act
            var log = state.Spawn(f.GroundEntityType, position, out var entityB);

            // Assert
            Assert.That(log, Is.Null);
            Assert.That(entityB, Is.Null);
            var entityAFound = state.Board.TryGetCoords(entityA, out var entityAPosition);
            Assert.That(entityAFound, Is.True);
            Assert.That(entityAPosition, Is.EqualTo(position));
        }

        [Test]
        public void SpawnFailsWithIncompatibleTile()
        {
            // Arrange
            var f = new ContentFixture();
            var state = new GameState(new Size(3, 3), f.WallTileType);
            var position = new Coords(1, 1);

            // Act
            var log = state.Spawn(f.GroundEntityType, position, out var entity);

            // Assert
            Assert.That(log, Is.Null);
            Assert.That(entity, Is.Null);
            var entities = state.Board.GetEntities(position);
            Assert.That(entities, Is.Empty);
        }
    }
}