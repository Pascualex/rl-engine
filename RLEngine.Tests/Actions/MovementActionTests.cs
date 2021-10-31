using RLEngine.Core.Actions;
using RLEngine.Core.Turns;
using RLEngine.Core.Boards;
using RLEngine.Core.Entities;
using RLEngine.Core.Utils;

using Xunit;
using FluentAssertions;
using NSubstitute;

namespace RLEngine.Tests.Actions
{
    public class MovementActionTests
    {
        [Fact]
        public void MovementActionPasses()
        {
            var position = Coords.Zero;

            // Arrange
            var entity = Substitute.For<IEntity>();
            entity.IsActive.Returns(true);
            var board = Substitute.For<IBoard>();
            board.Move(entity, position).Returns(true);
            var turnManager = Substitute.For<ITurnManager>();
            var executor = new ActionExecutor(turnManager, board);

            // Act
            var log = executor.Move(entity, position)!;

            // Assert
            log.Should().NotBeNull();
            log.Entity.Should().Be(entity);
            log.To.Should().Be(position);
            entity.DidNotReceive().OnMove(Arg.Any<Coords>());
            board.Received().Move(entity, position);
        }

        [Fact]
        public void MovementActionFailsWithNonActiveEntity()
        {
            var position = Coords.Zero;

            // Arrange
            var entity = Substitute.For<IEntity>();
            entity.IsActive.Returns(false);
            var board = Substitute.For<IBoard>();
            board.Move(entity, position).Returns(true);
            var turnManager = Substitute.For<ITurnManager>();
            var executor = new ActionExecutor(turnManager, board);

            // Act
            var log = executor.Move(entity, position);

            // Assert
            log.Should().BeNull();
            entity.DidNotReceive().OnMove(Arg.Any<Coords>());
            board.DidNotReceive().Move(Arg.Any<Entity>(), Arg.Any<Coords>());
        }

        [Fact]
        public void DestructionActionFailsWhenBoardCanNotMove()
        {
            var position = Coords.Zero;

            // Arrange
            var entity = Substitute.For<IEntity>();
            entity.IsActive.Returns(true);
            var board = Substitute.For<IBoard>();
            board.Move(entity, position).Returns(false);
            var turnManager = Substitute.For<ITurnManager>();
            var executor = new ActionExecutor(turnManager, board);

            // Act
            var log = executor.Move(entity, position);

            // Assert
            log.Should().BeNull();
            entity.DidNotReceive().OnMove(Arg.Any<Coords>());
            board.Received().Move(entity, position);
        }
    }
}