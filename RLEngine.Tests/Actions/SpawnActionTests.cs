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
    public class SpawnActionTests
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void SpawnActionPasses(bool isAgent)
        {
            var position = Coords.Zero;

            // Arrange
            var entityType = new EntityType { IsAgent = isAgent };
            var board = Substitute.For<IBoard>();
            board.CanAdd(Arg.Is<Entity>(x => x.Type == entityType), position).Returns(true);
            var turnManager = Substitute.For<ITurnManager>();
            var executor = new ActionExecutor(turnManager, board);

            // Act
            var log = executor.Spawn(entityType, position)!;

            // Assert
            log.Should().NotBeNull();
            log.Entity.Type.Should().Be(entityType);
            log.Entity.IsActive.Should().Be(false);
            log.At.Should().Be(position);
            board.Received().Add(Arg.Is<Entity>(x => x.Type == entityType), position);
            if (isAgent) turnManager.Received().Add(Arg.Is<Entity>(x => x.Type == entityType));
            else turnManager.DidNotReceive().Add(Arg.Any<Entity>());
        }

        [Fact]
        public void SpawnActionFailsWhenBoardCanNotAdd()
        {
            var position = Coords.Zero;

            // Arrange
            var entityType = new EntityType();
            var board = Substitute.For<IBoard>();
            board.CanAdd(Arg.Is<Entity>(x => x.Type == entityType), position).Returns(false);
            var turnManager = Substitute.For<ITurnManager>();
            var executor = new ActionExecutor(turnManager, board);

            // Act
            var log = executor.Spawn(entityType, position);

            // Assert
            log.Should().BeNull();
            board.DidNotReceive().Add(Arg.Any<Entity>(), position);
            turnManager.DidNotReceive().Add(Arg.Any<Entity>());
        }
    }
}