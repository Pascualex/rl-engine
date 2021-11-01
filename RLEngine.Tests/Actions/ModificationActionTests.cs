using RLEngine.Core.Actions;
using RLEngine.Core.Turns;
using RLEngine.Core.Boards;
using RLEngine.Core.Utils;

using Xunit;
using FluentAssertions;
using NSubstitute;

namespace RLEngine.Tests.Actions
{
    public class ModificationActionTests
    {
        [Fact]
        public void ModifyPasses()
        {
            var position = Coords.Zero;

            // Arrange
            var tileTypeA = new TileType();
            var tileTypeB = new TileType();
            var board = Substitute.For<IBoard>();
            board.GetTileType(position).Returns(tileTypeA);
            board.CanModify(tileTypeB, position).Returns(true);
            var turnManager = Substitute.For<ITurnManager>();
            var executor = new ActionExecutor(turnManager, board);

            // Act
            var log = executor.Modify(tileTypeB, position)!;

            // Assert
            log.Should().NotBeNull();
            log.NewType.Should().Be(tileTypeB);
            log.PreviousType.Should().Be(tileTypeA);
            log.At.Should().Be(position);
            board.Received().Modify(tileTypeB, position);
        }

        [Fact]
        public void ModifyFailsWhenBoardCanNotModify()
        {
            var position = Coords.Zero;

            // Arrange
            var tileTypeA = new TileType();
            var tileTypeB = new TileType();
            var board = Substitute.For<IBoard>();
            board.GetTileType(position).Returns(tileTypeA);
            board.CanModify(tileTypeB, position).Returns(false);
            var turnManager = Substitute.For<ITurnManager>();
            var executor = new ActionExecutor(turnManager, board);

            // Act
            var log = executor.Modify(tileTypeB, position);

            // Assert
            log.Should().BeNull();
            board.DidNotReceive().Modify(Arg.Any<TileType>(), Arg.Any<Coords>());
        }
    }
}