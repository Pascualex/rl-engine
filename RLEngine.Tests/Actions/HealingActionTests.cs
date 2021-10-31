using RLEngine.Core.Actions;
using RLEngine.Core.Turns;
using RLEngine.Core.Boards;
using RLEngine.Core.Entities;

using Xunit;
using FluentAssertions;
using NSubstitute;

namespace RLEngine.Tests.Actions
{
    public class HealingActionTests
    {
        [Fact]
        public void HealingActionPassesWithoutAttacker()
        {
            const int healing = 20;
            const int actualHealing = 10;

            // Arrange
            var target = Substitute.For<IEntity>();
            target.IsActive.Returns(true);
            target.Heal(healing).Returns(actualHealing);
            var amount = Substitute.For<IAmount>();
            amount.Calculate(target, null).Returns(healing);
            var board = Substitute.For<IBoard>();
            var turnManager = Substitute.For<ITurnManager>();
            var executor = new ActionExecutor(turnManager, board);

            // Act
            var log = executor.Heal(target, amount)!;

            // Assert
            log.Should().NotBeNull();
            log.Target.Should().Be(target);
            log.Healer.Should().BeNull();
            log.Healing.Should().Be(healing);
            log.ActualHealing.Should().Be(actualHealing);
            target.Received().Heal(healing);
        }

        [Fact]
        public void HealingActionPassesWithAttacker()
        {
            const int healing = 20;
            const int actualHealing = 10;

            // Arrange
            var target = Substitute.For<IEntity>();
            target.IsActive.Returns(true);
            target.Heal(healing).Returns(actualHealing);
            var healer = Substitute.For<IEntity>();
            healer.IsActive.Returns(true);
            var amount = Substitute.For<IAmount>();
            amount.Calculate(target, healer).Returns(healing);
            var board = Substitute.For<IBoard>();
            var turnManager = Substitute.For<ITurnManager>();
            var executor = new ActionExecutor(turnManager, board);

            // Act
            var log = executor.Heal(target, healer, amount)!;

            // Assert
            log.Should().NotBeNull();
            log.Target.Should().Be(target);
            log.Healer.Should().Be(healer);
            log.Healing.Should().Be(healing);
            log.ActualHealing.Should().Be(actualHealing);
            target.Received().Heal(healing);
        }

        [Fact]
        public void HealingActionFailsWithNotActiveTarget()
        {
            // Arrange
            var target = Substitute.For<IEntity>();
            target.IsActive.Returns(false);
            var amount = Substitute.For<IAmount>();
            var board = Substitute.For<IBoard>();
            var turnManager = Substitute.For<ITurnManager>();
            var executor = new ActionExecutor(turnManager, board);

            // Act
            var log = executor.Heal(target, amount);

            // Assert
            log.Should().BeNull();
            target.DidNotReceive().Heal(Arg.Any<int>());
        }

        [Fact]
        public void HealingActionFailsWithNotActiveAttacker()
        {
            // Arrange
            var target = Substitute.For<IEntity>();
            target.IsActive.Returns(true);
            var healer = Substitute.For<IEntity>();
            healer.IsActive.Returns(false);
            var amount = Substitute.For<IAmount>();
            var board = Substitute.For<IBoard>();
            var turnManager = Substitute.For<ITurnManager>();
            var executor = new ActionExecutor(turnManager, board);

            // Act
            var log = executor.Heal(target, healer, amount);

            // Assert
            log.Should().BeNull();
            target.DidNotReceive().Heal(Arg.Any<int>());
        }
    }
}