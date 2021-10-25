using RLEngine.Events;
using RLEngine.Logs;
using RLEngine.Turns;
using RLEngine.Boards;
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
            var board = new Board(new Size(3, 3), f.FloorTileType);
            var ctx = new EventContext(new EventQueue(), new TurnManager(), board);
            var position = new Coords(x, y);

            // Act
            var log = ctx.Spawn(f.AgentType, position, out var entity);

            // Assert
            var spawnLog = (SpawnLog)log.FailIfNull();
            Assert.That(spawnLog.Entity, Is.SameAs(entity));
            Assert.That(spawnLog.At, Is.EqualTo(position));
            entity = entity.FailIfNull();
            var currentEntity = ctx.TurnManager.Current;
            Assert.That(currentEntity, Is.SameAs(entity));
            Assert.That(entity.Position, Is.EqualTo(position));
        }

        [Test]
        [TestCase(0, -1)]
        [TestCase(-1, -1)]
        [TestCase(3, 0)]
        public void SpawnFailsOutOfBounds(int x, int y)
        {
            // Arrange
            var f = new ContentFixture();
            var board = new Board(new Size(3, 3), f.FloorTileType);
            var ctx = new EventContext(new EventQueue(), new TurnManager(), board);
            var position = new Coords(x, y);

            // Act
            var log = ctx.Spawn(f.GroundEntityType, position, out var entity);

            // Assert
            Assert.That(log, Is.Null);
            Assert.That(entity, Is.Null);
            var currentEntity = ctx.TurnManager.Current;
            Assert.That(currentEntity, Is.Null);
            var entities = ctx.Board.GetEntities(position);
            Assert.That(entities, Is.Empty);
        }

        [Test]
        public void SpawnPassesWithCompatibleEntity()
        {
            // Arrange
            var f = new ContentFixture();
            var board = new Board(new Size(3, 3), f.FloorTileType);
            var ctx = new EventContext(new EventQueue(), new TurnManager(), board);
            var position = new Coords(1, 1);
            ctx.Spawn(f.GroundEntityType, position, out var entityA);
            entityA = entityA.FailIfNull();

            // Act
            var log = ctx.Spawn(f.GhostAgentType, position, out var entityB);

            // Assert
            var spawnLog = (SpawnLog)log.FailIfNull();
            Assert.That(spawnLog.Entity, Is.SameAs(entityB));
            Assert.That(spawnLog.At, Is.EqualTo(position));
            Assert.That(entityA.Position, Is.EqualTo(position));
            entityB = entityB.FailIfNull();
            Assert.That(entityB.Position, Is.EqualTo(position));
        }

        [Test]
        public void SpawnFailsWithIncompatibleEntity()
        {
            // Arrange
            var f = new ContentFixture();
            var board = new Board(new Size(3, 3), f.FloorTileType);
            var ctx = new EventContext(new EventQueue(), new TurnManager(), board);
            var position = new Coords(1, 1);
            ctx.Spawn(f.GroundEntityType, position, out var entityA);
            entityA = entityA.FailIfNull();

            // Act
            var log = ctx.Spawn(f.GroundEntityType, position, out var entityB);

            // Assert
            Assert.That(log, Is.Null);
            Assert.That(entityB, Is.Null);
            Assert.That(entityA.Position, Is.EqualTo(position));
        }

        [Test]
        public void SpawnFailsWithIncompatibleTile()
        {
            // Arrange
            var f = new ContentFixture();
            var board = new Board(new Size(3, 3), f.WallTileType);
            var ctx = new EventContext(new EventQueue(), new TurnManager(), board);
            var position = new Coords(1, 1);

            // Act
            var log = ctx.Spawn(f.GroundEntityType, position, out var entity);

            // Assert
            Assert.That(log, Is.Null);
            Assert.That(entity, Is.Null);
            var entities = ctx.Board.GetEntities(position);
            Assert.That(entities, Is.Empty);
        }
    }
}