using RLEngine.Core.Turns;
using RLEngine.Core.Entities;

using Xunit;
using FluentAssertions;
using NSubstitute;

namespace RLEngine.Tests.Turns
{
    public class TurnManagerTests
    {
        [Fact]
        public void TurnManagerAddPassesWithAgent()
        {
            // Arrange
            var entity = Substitute.For<IEntity>();
            entity.IsAgent.Returns(true);
            var turnManager = new TurnManager();

            // Act
            turnManager.Add(entity);

            // Assert
            turnManager.Current.Should().Be(entity);
        }

        [Fact]
        public void TurnManagerAddFailsWithNonAgent()
        {
            // Arrange
            var entity = Substitute.For<IEntity>();
            entity.IsAgent.Returns(false);
            var turnManager = new TurnManager();

            // Act
            turnManager.Add(entity);

            // Assert
            turnManager.Current.Should().BeNull();
        }

        [Fact]
        public void TurnManagerAddPassesWithSameSpeedAgent()
        {
            // Arrange
            var entityA = Substitute.For<IEntity>();
            entityA.IsAgent.Returns(true);
            entityA.Speed.Returns(100);
            var entityB = Substitute.For<IEntity>();
            entityB.IsAgent.Returns(true);
            entityB.Speed.Returns(100);
            var turnManager = new TurnManager();
            turnManager.Add(entityA);

            // Act
            turnManager.Add(entityB);

            // Assert
            turnManager.Current.Should().Be(entityA);
        }

        [Fact]
        public void TurnManagerAddPassesWithSlowerAgent()
        {
            // Arrange
            var entityA = Substitute.For<IEntity>();
            entityA.IsAgent.Returns(true);
            entityA.Speed.Returns(100);
            var entityB = Substitute.For<IEntity>();
            entityB.IsAgent.Returns(true);
            entityB.Speed.Returns(80);
            var turnManager = new TurnManager();
            turnManager.Add(entityA);

            // Act
            turnManager.Add(entityB);

            // Assert
            turnManager.Current.Should().Be(entityA);
        }

        [Fact]
        public void TurnManagerAddPassesWithFasterAgent()
        {
            // Arrange
            var entityA = Substitute.For<IEntity>();
            entityA.IsAgent.Returns(true);
            entityA.Speed.Returns(100);
            var entityB = Substitute.For<IEntity>();
            entityB.IsAgent.Returns(true);
            entityB.Speed.Returns(120);
            var turnManager = new TurnManager();
            turnManager.Add(entityA);

            // Act
            turnManager.Add(entityB);

            // Assert
            turnManager.Current.Should().Be(entityB);
        }

        [Fact]
        public void TurnManagerNextPasses()
        {
            // Arrange
            var entity = Substitute.For<IEntity>();
            entity.IsAgent.Returns(true);
            entity.Speed.Returns(100);
            var turnManager = new TurnManager();
            turnManager.Add(entity);

            // Act
            turnManager.Next(100);

            // Assert
            turnManager.Current.Should().Be(entity);
        }

        [Fact]
        public void TurnManagerNextPassesWhileEmpty()
        {
            // Arrange
            var turnManager = new TurnManager();

            // Act
            turnManager.Next(100);

            // Assert
            turnManager.Current.Should().BeNull();
        }

        [Fact]
        public void TurnManagerNextPassesWithSameSpeedAgent()
        {
            // Arrange
            var entityA = Substitute.For<IEntity>();
            entityA.IsAgent.Returns(true);
            entityA.Speed.Returns(100);
            var entityB = Substitute.For<IEntity>();
            entityB.IsAgent.Returns(true);
            entityB.Speed.Returns(100);
            var turnManager = new TurnManager();
            turnManager.Add(entityA);
            turnManager.Add(entityB);

            // Act
            turnManager.Next(100);

            // Assert
            turnManager.Current.Should().Be(entityB);
        }

        [Fact]
        public void TurnManagerNextPassesWithFasterAgent()
        {
            // Arrange
            var entityA = Substitute.For<IEntity>();
            entityA.IsAgent.Returns(true);
            entityA.Speed.Returns(200);
            var entityB = Substitute.For<IEntity>();
            entityB.IsAgent.Returns(true);
            entityB.Speed.Returns(50);
            var turnManager = new TurnManager();
            turnManager.Add(entityA);
            turnManager.Add(entityB);

            // Act
            turnManager.Next(100); // entityA -> entityB
            turnManager.Next(100); // entityB -> entityA
            turnManager.Next(100); // entityA -> entityA

            // Assert
            turnManager.Current.Should().Be(entityA);
        }

        [Fact]
        public void TurnManagerNextPassesWithAgentAddedLater()
        {
            // Arrange
            var entityA = Substitute.For<IEntity>();
            entityA.IsAgent.Returns(true);
            entityA.Speed.Returns(100);
            var entityB = Substitute.For<IEntity>();
            entityB.IsAgent.Returns(true);
            entityB.Speed.Returns(100);
            var turnManager = new TurnManager();
            turnManager.Add(entityA);
            turnManager.Next(100); // entityA -> entityA
            turnManager.Add(entityB);

            // Act
            turnManager.Next(100); // entityA -> entityB

            // Assert
            turnManager.Current.Should().Be(entityB);
        }

        [Fact]
        public void TurnManagerRemovePasses()
        {
            // Arrange
            var entity = Substitute.For<IEntity>();
            entity.IsAgent.Returns(true);
            var turnManager = new TurnManager();
            turnManager.Add(entity);

            // Act
            turnManager.Remove(entity);

            // Assert
            turnManager.Current.Should().BeNull();
        }

        [Fact]
        public void TurnManagerRemoveFailsWhenAgentIsNotAdded()
        {
            // Arrange
            var entity = Substitute.For<IEntity>();
            entity.IsAgent.Returns(true);
            var turnManager = new TurnManager();

            // Act
            turnManager.Remove(entity);

            // Assert
            turnManager.Current.Should().BeNull();
        }
    }
}