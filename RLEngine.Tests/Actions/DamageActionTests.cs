using RLEngine.Core.Actions;
using RLEngine.Core.Turns;
using RLEngine.Core.Boards;
using RLEngine.Core.Entities;

using Xunit;
using FluentAssertions;
using NSubstitute;

namespace RLEngine.Tests.Actions
{
    public class DamageActionTests
    {
        [Fact]
        public void DamageActionPassesWithoutAttacker()
        {
            const int damage = 20;
            const int actualDamage = 10;

            // Arrange
            var target = Substitute.For<IEntity>();
            target.IsActive.Returns(true);
            target.Damage(damage).Returns(actualDamage);
            var amount = new Amount { Base = damage };
            var board = Substitute.For<IBoard>();
            var turnManager = Substitute.For<ITurnManager>();
            var executor = new ActionExecutor(turnManager, board);

            // Act
            var log = executor.Damage(target, amount)!;

            // Assert
            log.Should().NotBeNull();
            log.Target.Should().Be(target);
            log.Attacker.Should().BeNull();
            log.Damage.Should().Be(damage);
            log.ActualDamage.Should().Be(actualDamage);
            target.Received().Damage(damage);
        }

        [Fact]
        public void DamageActionPassesWithAttacker()
        {
            const int damage = 20;
            const int actualDamage = 10;

            // Arrange
            var target = Substitute.For<IEntity>();
            target.IsActive.Returns(true);
            target.Damage(damage).Returns(actualDamage);
            var attacker = Substitute.For<IEntity>();
            attacker.IsActive.Returns(true);
            var amount = new Amount { Base = damage };
            var board = Substitute.For<IBoard>();
            var turnManager = Substitute.For<ITurnManager>();
            var executor = new ActionExecutor(turnManager, board);

            // Act
            var log = executor.Damage(target, attacker, amount)!;

            // Assert
            log.Should().NotBeNull();
            log.Target.Should().Be(target);
            log.Attacker.Should().Be(attacker);
            log.Damage.Should().Be(damage);
            log.ActualDamage.Should().Be(actualDamage);
            target.Received().Damage(damage);
        }

        [Fact]
        public void DamageActionFailsWithNotActiveTarget()
        {
            // Arrange
            var target = Substitute.For<IEntity>();
            target.IsActive.Returns(false);
            var amount = new Amount();
            var board = Substitute.For<IBoard>();
            var turnManager = Substitute.For<ITurnManager>();
            var executor = new ActionExecutor(turnManager, board);

            // Act
            var log = executor.Damage(target, amount);

            // Assert
            log.Should().BeNull();
            target.DidNotReceive().Damage(Arg.Any<int>());
        }

        [Fact]
        public void DamageActionFailsWithNotActiveAttacker()
        {
            // Arrange
            var target = Substitute.For<IEntity>();
            target.IsActive.Returns(true);
            var attacker = Substitute.For<IEntity>();
            attacker.IsActive.Returns(false);
            var amount = new Amount();
            var board = Substitute.For<IBoard>();
            var turnManager = Substitute.For<ITurnManager>();
            var executor = new ActionExecutor(turnManager, board);

            // Act
            var log = executor.Damage(target, attacker, amount);

            // Assert
            log.Should().BeNull();
            target.DidNotReceive().Damage(Arg.Any<int>());
        }
    }
}