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
            var f = new ContentFixture();

            var state = new GameState(new Size(3, 3), f.FloorTileType);

            var initialPosition = new Coords(ix, iy);
            state.Spawn(f.GroundEntityType, initialPosition, out var entity);

            entity = entity.FailIfNull();
            var finalPosition = new Coords(fx, fy);
            var log = state.Move(entity, finalPosition, relative);
            var moveLog = (MoveLog)log.FailIfNull();
            Assert.That(moveLog.Entity, Is.SameAs(entity));
            if (relative) finalPosition += initialPosition;
            Assert.That(moveLog.To, Is.EqualTo(finalPosition));

            var entityPosition = state.Board.GetCoords(entity);
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
            var f = new ContentFixture();

            var board = new Board(new Size(3, 3), f.FloorTileType);
            var entity = new Entity(f.GroundEntityType);

            var initialPosition = new Coords(ix, iy);
            board.Add(entity, initialPosition);

            var finalPosition = new Coords(fx, fy);
            var moved = board.Move(entity, finalPosition, relative);
            Assert.That(moved, Is.False);

            var entityPosition = board.GetCoords(entity);
            Assert.That(entityPosition, Is.EqualTo(initialPosition));
        }

        [Test]
        public void MoveFailsWhenEntityIsNotAdded()
        {
            var f = new ContentFixture();

            var state = new GameState(new Size(3, 3), f.FloorTileType);
            var entity = new Entity(f.GroundEntityType);

            var finalPosition = new Coords(1, 1);
            var log = state.Move(entity, finalPosition, false);
            Assert.That(log, Is.Null);

            var entityPosition = state.Board.GetCoords(entity);
            Assert.That(entityPosition, Is.Null);
        }

        [Test]
        public void MovePassesWithCompatibleEntity()
        {
            var f = new ContentFixture();

            var state = new GameState(new Size(3, 3), f.FloorTileType);

            var finalPosition = new Coords(2, 1);
            state.Spawn(f.GroundEntityType, finalPosition, out var entityA);

            var initialPosition = new Coords(0, 1);
            state.Spawn(f.GhostAgentType, initialPosition, out var entityB);
            entityB = entityB.FailIfNull();
            var log = state.Move(entityB, finalPosition, false);
            var moveLog = (MoveLog)log.FailIfNull();
            Assert.That(moveLog.Entity, Is.SameAs(entityB));
            Assert.That(moveLog.To, Is.EqualTo(finalPosition));

            entityA = entityA.FailIfNull();
            var entityAPosition = state.Board.GetCoords(entityA);
            Assert.That(entityAPosition, Is.EqualTo(finalPosition));

            var entityBPosition = state.Board.GetCoords(entityB);
            Assert.That(entityBPosition, Is.EqualTo(finalPosition));
        }

        [Test]
        public void MoveFailsWithIncompatibleEntity()
        {
            var f = new ContentFixture();

            var state = new GameState(new Size(3, 3), f.FloorTileType);

            var finalPosition = new Coords(2, 1);
            state.Spawn(f.GroundEntityType, finalPosition, out var entityA);

            var initialPosition = new Coords(0, 1);
            state.Spawn(f.GroundEntityType, initialPosition, out var entityB);
            entityB = entityB.FailIfNull();
            var log = state.Move(entityB, finalPosition, false);
            Assert.That(log, Is.Null);

            entityA = entityA.FailIfNull();
            var entityAPosition = state.Board.GetCoords(entityA);
            Assert.That(entityAPosition, Is.EqualTo(finalPosition));

            var entityBPosition = state.Board.GetCoords(entityB);
            Assert.That(entityBPosition, Is.EqualTo(initialPosition));
        }

        [Test]
        public void MoveFailsWithIncompatibleTile()
        {
            var f = new ContentFixture();

            var state = new GameState(new Size(3, 3), f.FloorTileType);

            var initialPosition = new Coords(0, 1);
            state.Spawn(f.GroundEntityType, initialPosition, out var entity);

            var finalPosition = new Coords(2, 1);
            state.Modify(f.WallTileType, finalPosition);
            entity = entity.FailIfNull();
            var log = state.Move(entity, finalPosition, false);
            Assert.That(log, Is.Null);

            var entityPosition = state.Board.GetCoords(entity);
            Assert.That(entityPosition, Is.EqualTo(initialPosition));
        }
    }
}