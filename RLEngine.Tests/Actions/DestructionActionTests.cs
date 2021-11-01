using RLEngine.Core.Actions;
using RLEngine.Core.Turns;
using RLEngine.Core.Boards;
using RLEngine.Core.Entities;

using Xunit;
using FluentAssertions;
using NSubstitute;

namespace RLEngine.Tests.Actions
{
    public class DestructionActionTests
    {
        [Fact]
        public void DestructionActionPasses()
        {
            // Arrange
            var entity = Substitute.For<IEntity>();
            entity.IsActive.Returns(true);
            var board = Substitute.For<IBoard>();
            var turnManager = Substitute.For<ITurnManager>();
            var executor = new ActionExecutor(turnManager, board);

            // Act
            var log = executor.Destroy(entity)!;

            // Assert
            log.Should().NotBeNull();
            log.Entity.Should().Be(entity);
            entity.DidNotReceive().OnDestroy();
            board.Received().Remove(entity);
            turnManager.Received().Remove(entity);
        }

        [Fact]
        public void DestructionActionFailsWithNotActiveEntity()
        {
            // Arrange
            var entity = Substitute.For<IEntity>();
            entity.IsActive.Returns(false);
            var board = Substitute.For<IBoard>();
            var turnManager = Substitute.For<ITurnManager>();
            var executor = new ActionExecutor(turnManager, board);

            // Act
            var log = executor.Destroy(entity);

            // Assert
            log.Should().BeNull();
            entity.DidNotReceive().OnDestroy();
            board.DidNotReceive().Remove(Arg.Any<Entity>());
            turnManager.DidNotReceive().Remove(Arg.Any<Entity>());
        }
    }
}