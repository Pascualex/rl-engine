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
            var board = new Board(new Size(3, 3), f.FloorTileType);
            var ctx = new EventContext(new EventQueue(), new TurnManager(), board);
            var position = new Coords(x, y);

            // Act
            var log = ctx.Modify(f.WallTileType, position);

            // Assert
            var modificationLog = (ModificationLog)log.FailIfNull();
            Assert.That(modificationLog.NewType, Is.SameAs(f.WallTileType));
            Assert.That(modificationLog.PreviousType, Is.SameAs(f.FloorTileType));
            Assert.That(modificationLog.At, Is.EqualTo(position));
            var tileType = ctx.Board.GetTileType(position);
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
            var board = new Board(new Size(3, 3), f.FloorTileType);
            var ctx = new EventContext(new EventQueue(), new TurnManager(), board);
            var position = new Coords(x, y);

            // Act
            var log = ctx.Modify(f.WallTileType, position);

            // Assert
            Assert.That(log, Is.Null);
            var tileType = ctx.Board.GetTileType(position);
            Assert.That(tileType, Is.Null);
        }
    }
}