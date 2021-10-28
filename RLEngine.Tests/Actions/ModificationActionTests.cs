using RLEngine.Actions;
using RLEngine.Logs;
using RLEngine.Turns;
using RLEngine.Boards;
using RLEngine.Utils;
using RLEngine.Tests.Utils;

using NUnit.Framework;

namespace RLEngine.Tests.Actions
{
    [TestFixture]
    public class ModificationActionTests
    {
        [Test]
        [TestCase(0, 0)]
        [TestCase(0, 1)]
        [TestCase(2, 2)]
        public void ModifyPasses(int x, int y)
        {
            // Arrange
            var f = new ContentFixture();
            var turnManager = new TurnManager();
            var board = new Board(new Size(3, 3), f.FloorTileType);
            var executor = new ActionExecutor(turnManager, board);
            var position = new Coords(x, y);

            // Act
            var log = executor.Modify(f.WallTileType, position);

            // Assert
            log = log.FailIfNull();
            Assert.That(log.NewType, Is.SameAs(f.WallTileType));
            Assert.That(log.PreviousType, Is.SameAs(f.FloorTileType));
            Assert.That(log.At, Is.EqualTo(position));
            var tileType = board.GetTileType(position);
            Assert.That(tileType, Is.SameAs(f.WallTileType));
        }

        [Test]
        [TestCase(0, -1)]
        [TestCase(-1, -1)]
        [TestCase(3, 0)]
        [TestCase(20, 30)]
        public void ModifyFailsOutOfBounds(int x, int y)
        {
            // Arrange
            var f = new ContentFixture();
            var turnManager = new TurnManager();
            var board = new Board(new Size(3, 3), f.FloorTileType);
            var executor = new ActionExecutor(turnManager, board);
            var position = new Coords(x, y);

            // Act
            var log = executor.Modify(f.WallTileType, position);

            // Assert
            Assert.That(log, Is.Null);
            var tileType = board.GetTileType(position);
            Assert.That(tileType, Is.Null);
        }
    }
}