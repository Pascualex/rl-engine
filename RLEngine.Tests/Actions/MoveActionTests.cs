using RLEngine.Actions;
using RLEngine.Logs;
using RLEngine.State;
using RLEngine.Boards;
using RLEngine.Entities;
using RLEngine.Utils;
using RLEngine.Tests.Utils;

using NUnit.Framework;

namespace RLEngine.Tests.Actions
{
    [TestFixture]
    public class MoveActionTests
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
            var state = new GameState(new Size(3, 3), f.FloorTileType);
            var initialPosition = new Coords(ix, iy);
            var finalPosition = new Coords(fx, fy);
            state.Spawn(f.GroundEntityType, initialPosition, out var entity);
            entity = entity.FailIfNull();

            // Act
            var log = state.Move(entity, finalPosition, relative);

            // Assert
            if (relative) finalPosition += initialPosition;
            var moveLog = (MoveLog)log.FailIfNull();
            Assert.That(moveLog.Entity, Is.SameAs(entity));
            Assert.That(moveLog.To, Is.EqualTo(finalPosition));
            var found = state.Board.TryGetCoords(entity, out var entityPosition);
            Assert.That(found, Is.True);
            Assert.That(entityPosition, Is.EqualTo(finalPosition));
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
            var state = new GameState(new Size(3, 3), f.FloorTileType);
            var initialPosition = new Coords(ix, iy);
            var finalPosition = new Coords(fx, fy);
            state.Spawn(f.GroundEntityType, initialPosition, out var entity);
            entity = entity.FailIfNull();

            // Act
            var log = state.Move(entity, finalPosition, relative);

            // Assert
            Assert.That(log, Is.Null);
            var found = state.Board.TryGetCoords(entity, out var entityPosition);
            Assert.That(found, Is.True);
            Assert.That(entityPosition, Is.EqualTo(initialPosition));
        }

        [Test]
        public void MoveFailsWhenEntityIsNotAdded()
        {
            // Arrange
            var f = new ContentFixture();
            var state = new GameState(new Size(3, 3), f.FloorTileType);
            var finalPosition = new Coords(1, 1);
            var entity = new Entity(f.GroundEntityType);

            // Act
            var log = state.Move(entity, finalPosition, false);

            // Assert
            Assert.That(log, Is.Null);
            var found = state.Board.TryGetCoords(entity, out _);
            Assert.That(found, Is.False);
        }

        [Test]
        public void MovePassesWithCompatibleEntity()
        {
            // Arrange
            var f = new ContentFixture();
            var state = new GameState(new Size(3, 3), f.FloorTileType);
            var initialPosition = new Coords(0, 1);
            var finalPosition = new Coords(2, 1);
            state.Spawn(f.GroundEntityType, finalPosition, out var entityA);
            entityA = entityA.FailIfNull();
            state.Spawn(f.GhostAgentType, initialPosition, out var entityB);
            entityB = entityB.FailIfNull();

            // Act
            var log = state.Move(entityB, finalPosition, false);

            // Assert
            var moveLog = (MoveLog)log.FailIfNull();
            Assert.That(moveLog.Entity, Is.SameAs(entityB));
            Assert.That(moveLog.To, Is.EqualTo(finalPosition));
            var entityAFound = state.Board.TryGetCoords(entityA, out var entityAPosition);
            Assert.That(entityAFound, Is.True);
            Assert.That(entityAPosition, Is.EqualTo(finalPosition));
            var entityBFound = state.Board.TryGetCoords(entityB, out var entityBPosition);
            Assert.That(entityBFound, Is.True);
            Assert.That(entityBPosition, Is.EqualTo(finalPosition));
        }

        [Test]
        public void MoveFailsWithIncompatibleEntity()
        {
            // Arrange
            var f = new ContentFixture();
            var state = new GameState(new Size(3, 3), f.FloorTileType);
            var initialPosition = new Coords(0, 1);
            var finalPosition = new Coords(2, 1);
            state.Spawn(f.GroundEntityType, finalPosition, out var entityA);
            entityA = entityA.FailIfNull();
            state.Spawn(f.GroundEntityType, initialPosition, out var entityB);
            entityB = entityB.FailIfNull();

            // Act
            var log = state.Move(entityB, finalPosition, false);

            // Assert
            Assert.That(log, Is.Null);
            var entityAFound = state.Board.TryGetCoords(entityA, out var entityAPosition);
            Assert.That(entityAFound, Is.True);
            Assert.That(entityAPosition, Is.EqualTo(finalPosition));
            var entityBFound = state.Board.TryGetCoords(entityB, out var entityBPosition);
            Assert.That(entityBFound, Is.True);
            Assert.That(entityBPosition, Is.EqualTo(initialPosition));
        }

        [Test]
        public void MoveFailsWithIncompatibleTile()
        {
            // Arrange
            var f = new ContentFixture();
            var state = new GameState(new Size(3, 3), f.FloorTileType);
            var initialPosition = new Coords(0, 1);
            var finalPosition = new Coords(2, 1);
            state.Spawn(f.GroundEntityType, initialPosition, out var entity);
            entity = entity.FailIfNull();
            state.Modify(f.WallTileType, finalPosition);

            // Act
            var log = state.Move(entity, finalPosition, false);

            // Assert
            Assert.That(log, Is.Null);
            var found = state.Board.TryGetCoords(entity, out var entityPosition);
            Assert.That(found, Is.True);
            Assert.That(entityPosition, Is.EqualTo(initialPosition));
        }
    }
}