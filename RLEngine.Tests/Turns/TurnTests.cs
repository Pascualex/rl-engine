using RLEngine.Turns;
using RLEngine.Entities;
using RLEngine.Tests.Utils;

using NUnit.Framework;

namespace RLEngine.Tests.Turns
{
    [TestFixture]
    public class TurnTests
    {
        [Test]
        public void AddAgentPasses()
        {
            // Arrange
            var f = new ContentFixture();
            var turnManager = new TurnManager();
            var entity = new Entity(f.AgentType);

            // Act
            var added = turnManager.Add(entity);

            // Assert
            Assert.That(added, Is.True);
            var currentEntity = turnManager.Current;
            Assert.That(currentEntity, Is.SameAs(entity));
        }

        [Test]
        public void AddNonAgentFails()
        {
            // Arrange
            var f = new ContentFixture();
            var turnManager = new TurnManager();
            var entity = new Entity(f.EntityType);

            // Act
            var added = turnManager.Add(entity);

            // Assert
            Assert.That(added, Is.False);
            var currentEntity = turnManager.Current;
            Assert.That(currentEntity, Is.Null);
        }

        [Test]
        public void AddSameSpeedAgentPasses()
        {
            // Arrange
            var f = new ContentFixture();
            var turnManager = new TurnManager();
            var entityA = new Entity(f.SlowAgentType);
            var entityB = new Entity(f.SlowAgentType);
            turnManager.Add(entityA);

            // Act
            var added = turnManager.Add(entityB);

            // Assert
            Assert.That(added, Is.True);
            var currentEntity = turnManager.Current;
            Assert.That(currentEntity, Is.SameAs(entityA));
        }

        [Test]
        public void AddSlowerAgentPasses()
        {
            // Arrange
            var f = new ContentFixture();
            var turnManager = new TurnManager();
            var entityA = new Entity(f.FastAgentType);
            var entityB = new Entity(f.SlowAgentType);
            turnManager.Add(entityA);

            // Act
            var added = turnManager.Add(entityB);

            // Assert
            Assert.That(added, Is.True);
            var currentEntity = turnManager.Current;
            Assert.That(currentEntity, Is.SameAs(entityA));
        }

        [Test]
        public void AddFasterAgentPasses()
        {
            // Arrange
            var f = new ContentFixture();
            var turnManager = new TurnManager();
            var entityA = new Entity(f.SlowAgentType);
            var entityB = new Entity(f.FastAgentType);
            turnManager.Add(entityA);

            // Act
            var added = turnManager.Add(entityB);

            // Assert
            Assert.That(added, Is.True);
            var currentEntity = turnManager.Current;
            Assert.That(currentEntity, Is.SameAs(entityB));
        }

        [Test]
        public void NextPasses()
        {
            // Arrange
            var f = new ContentFixture();
            var turnManager = new TurnManager();
            var entityA = new Entity(f.SlowAgentType);
            var entityB = new Entity(f.SlowAgentType);
            turnManager.Add(entityA);
            turnManager.Add(entityB);

            // Act
            turnManager.Next(f.StandardActionCost);

            // Assert
            var currentEntity = turnManager.Current;
            Assert.That(currentEntity, Is.SameAs(entityB));
        }

        [Test]
        public void NextAlonePasses()
        {
            // Arrange
            var f = new ContentFixture();
            var turnManager = new TurnManager();
            var entity = new Entity(f.SlowAgentType);
            turnManager.Add(entity);

            // Act
            turnManager.Next(f.StandardActionCost);

            // Assert
            var currentEntity = turnManager.Current;
            Assert.That(currentEntity, Is.SameAs(entity));
        }

        [Test]
        public void NextEmptyPasses()
        {
            // Arrange
            var f = new ContentFixture();
            var turnManager = new TurnManager();

            // Act
            turnManager.Next(f.StandardActionCost);

            // Assert
            var currentEntity = turnManager.Current;
            Assert.That(currentEntity, Is.Null);
        }

        [Test]
        public void NextFastAgentPasses()
        {
            // Arrange
            var f = new ContentFixture();
            var turnManager = new TurnManager();
            var entityA = new Entity(f.FastAgentType);
            var entityB = new Entity(f.SlowAgentType);
            turnManager.Add(entityA);
            turnManager.Add(entityB);

            // Act
            turnManager.Next(f.StandardActionCost); // entityA -> entityB
            turnManager.Next(f.StandardActionCost); // entityB -> entityA
            turnManager.Next(f.StandardActionCost); // entityA -> entityA

            // Assert
            var currentEntity = turnManager.Current;
            Assert.That(currentEntity, Is.SameAs(entityA));
        }

        [Test]
        public void NextToAgentAddedLatePasses()
        {
            // Arrange
            var f = new ContentFixture();
            var turnManager = new TurnManager();
            var entityA = new Entity(f.SlowAgentType);
            var entityB = new Entity(f.SlowAgentType);
            turnManager.Add(entityA);
            turnManager.Next(f.StandardActionCost); // entityA -> entityA
            turnManager.Add(entityB);

            // Act
            turnManager.Next(f.StandardActionCost); // entityA -> entityB

            // Assert
            var currentEntity = turnManager.Current;
            Assert.That(currentEntity, Is.SameAs(entityB));
        }
    }
}