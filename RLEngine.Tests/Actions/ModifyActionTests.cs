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
    public class ModifyActionTests
    {
        [Test]
        [TestCase(0, 0)]
        [TestCase(0, 1)]
        [TestCase(2, 2)]
        public void ModifyPasses(int x, int y)
        {
            // Arrange
            var f = new ContentFixture();
            var state = new GameState(new Size(3, 3), f.FloorTileType);
            var position = new Coords(x, y);

            // Act
            var log = state.Modify(f.WallTileType, position);

            // Assert
            var modifyLog = (ModifyLog)log.FailIfNull();
            Assert.That(modifyLog.NewType, Is.SameAs(f.WallTileType));
            Assert.That(modifyLog.PreviousType, Is.SameAs(f.FloorTileType));
            Assert.That(modifyLog.At, Is.EqualTo(position));
            var tileType = state.Board.GetTileType(position);
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
            var state = new GameState(new Size(3, 3), f.FloorTileType);
            var position = new Coords(x, y);

            // Act
            var log = state.Modify(f.WallTileType, position);

            // Assert
            Assert.That(log, Is.Null);
            var tileType = state.Board.GetTileType(position);
            Assert.That(tileType, Is.Null);
        }
    }
}