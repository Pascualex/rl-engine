using RLEngine.Actions;
using RLEngine.Logs;
using RLEngine.Turns;
using RLEngine.Boards;
using RLEngine.Entities;
using RLEngine.Utils;
using RLEngine.Tests.Utils;

using NUnit.Framework;

namespace RLEngine.Tests.Actions
{
    [TestFixture]
    public class MovementActionTests
    {
        [Test]
        [TestCase(0, 1, 2,  0, false)]
        [TestCase(0, 1, 2, -1, true)]
        [TestCase(0, 0, 2,  2, false)]
        [TestCase(0, 0, 2,  2,  true)]
        [TestCase(1, 1, 1,  1, false)]
        [TestCase(1, 1, 0,  0, true)]
        public void MovePasses(int ix, int iy, int fx, int fy, bool relative)
        {
            // Arrange
            var f = new ContentFixture();
            var turnManager = new TurnManager();
            var board = new Board(new Size(3, 3), f.FloorTileType);
            var executor = new ActionExecutor(turnManager, board);
            var initialPosition = new Coords(ix, iy);
            var finalPosition = new Coords(fx, fy);
            executor.Spawn(f.GroundEntityType, initialPosition, out var entity);
            entity = entity.FailIfNull();

            // Act
            var log = executor.Move(entity, finalPosition, relative);

            // Assert
            if (relative) finalPosition += initialPosition;
            log = log.FailIfNull();
            Assert.That(log.Entity, Is.SameAs(entity));
            Assert.That(log.To, Is.EqualTo(finalPosition));
            Assert.That(entity.Position, Is.EqualTo(finalPosition));
        }

        [Test]
        [TestCase(0, 0,  0, -1, false)]
        [TestCase(0, 0,  0, -1,  true)]
        [TestCase(1, 1, -1, -1, false)]
        [TestCase(1, 1, -2, -2,  true)]
        [TestCase(1, 2,  3,  0, false)]
        [TestCase(1, 2,  2,  0,  true)]
        public void MoveFailsOutOfBounds(int ix, int iy, int fx, int fy, bool relative)
        {
            // Arrange
            var f = new ContentFixture();
            var turnManager = new TurnManager();
            var board = new Board(new Size(3, 3), f.FloorTileType);
            var executor = new ActionExecutor(turnManager, board);
            var initialPosition = new Coords(ix, iy);
            var finalPosition = new Coords(fx, fy);
            executor.Spawn(f.GroundEntityType, initialPosition, out var entity);
            entity = entity.FailIfNull();

            // Act
            var log = executor.Move(entity, finalPosition, relative);

            // Assert
            Assert.That(log, Is.Null);
            Assert.That(entity.Position, Is.EqualTo(initialPosition));
        }

        [Test]
        public void MoveFailsWhenEntityIsNotAdded()
        {
            // Arrange
            var f = new ContentFixture();
            var turnManager = new TurnManager();
            var board = new Board(new Size(3, 3), f.FloorTileType);
            var executor = new ActionExecutor(turnManager, board);
            var finalPosition = new Coords(1, 1);
            var entity = new Entity(f.GroundEntityType);

            // Act
            var log = executor.Move(entity, finalPosition, false);

            // Assert
            Assert.That(log, Is.Null);
        }

        [Test]
        public void MovePassesWithCompatibleEntity()
        {
            // Arrange
            var f = new ContentFixture();
            var turnManager = new TurnManager();
            var board = new Board(new Size(3, 3), f.FloorTileType);
            var executor = new ActionExecutor(turnManager, board);
            var initialPosition = new Coords(0, 1);
            var finalPosition = new Coords(2, 1);
            executor.Spawn(f.GroundEntityType, finalPosition, out var entityA);
            entityA = entityA.FailIfNull();
            executor.Spawn(f.GhostAgentType, initialPosition, out var entityB);
            entityB = entityB.FailIfNull();

            // Act
            var log = executor.Move(entityB, finalPosition, false);

            // Assert
            log = log.FailIfNull();
            Assert.That(log.Entity, Is.SameAs(entityB));
            Assert.That(log.To, Is.EqualTo(finalPosition));
            Assert.That(entityA.Position, Is.EqualTo(finalPosition));
            Assert.That(entityB.Position, Is.EqualTo(finalPosition));
        }

        [Test]
        public void MoveFailsWithIncompatibleEntity()
        {
            // Arrange
            var f = new ContentFixture();
            var turnManager = new TurnManager();
            var board = new Board(new Size(3, 3), f.FloorTileType);
            var executor = new ActionExecutor(turnManager, board);
            var initialPosition = new Coords(0, 1);
            var finalPosition = new Coords(2, 1);
            executor.Spawn(f.GroundEntityType, finalPosition, out var entityA);
            entityA = entityA.FailIfNull();
            executor.Spawn(f.GroundEntityType, initialPosition, out var entityB);
            entityB = entityB.FailIfNull();

            // Act
            var log = executor.Move(entityB, finalPosition, false);

            // Assert
            Assert.That(log, Is.Null);
            Assert.That(entityA.Position, Is.EqualTo(finalPosition));
            Assert.That(entityB.Position, Is.EqualTo(initialPosition));
        }

        [Test]
        public void MoveFailsWithIncompatibleTile()
        {
            // Arrange
            var f = new ContentFixture();
            var turnManager = new TurnManager();
            var board = new Board(new Size(3, 3), f.FloorTileType);
            var executor = new ActionExecutor(turnManager, board);
            var initialPosition = new Coords(0, 1);
            var finalPosition = new Coords(2, 1);
            executor.Spawn(f.GroundEntityType, initialPosition, out var entity);
            entity = entity.FailIfNull();
            executor.Modify(f.WallTileType, finalPosition);

            // Act
            var log = executor.Move(entity, finalPosition, false);

            // Assert
            Assert.That(log, Is.Null);
            Assert.That(entity.Position, Is.EqualTo(initialPosition));
        }
    }
}