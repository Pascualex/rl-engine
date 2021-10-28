using RLEngine.Actions;
using RLEngine.Turns;
using RLEngine.Boards;
using RLEngine.Entities;
using RLEngine.Utils;
using RLEngine.Tests.Utils;

using NUnit.Framework;

namespace RLEngine.Tests.Actions
{
    [TestFixture]
    public class DestructionActionTests
    {
        [Test]
        public void DestroyPasses()
        {
            // Arrange
            var f = new ContentFixture();
            var turnManager = new TurnManager();
            var board = new Board(new Size(3, 3), f.FloorTileType);
            var executor = new ActionExecutor(turnManager, board);
            var position = new Coords(1, 1);
            executor.Spawn(f.AgentType, position, out var entity);
            entity = entity.FailIfNull();

            // Act
            var log = executor.Destroy(entity);

            // Assert
            log = log.FailIfNull();
            Assert.That(log.Entity, Is.SameAs(entity));
            var currentEntity = turnManager.Current;
            Assert.That(currentEntity, Is.Null);
            var found = !board.CanAdd(entity, position);
            Assert.That(found, Is.False);
            Assert.That(entity.IsDestroyed, Is.True);
        }

        [Test]
        public void DestroyFailsWhenEntityIsNotAdded()
        {
            // Arrange
            var f = new ContentFixture();
            var turnManager = new TurnManager();
            var board = new Board(new Size(3, 3), f.FloorTileType);
            var executor = new ActionExecutor(turnManager, board);
            var entity = new Entity(f.AgentType);

            // Act
            var log = executor.Destroy(entity);

            // Assert
            Assert.That(log, Is.Null);
            var currentEntity = turnManager.Current;
            Assert.That(currentEntity, Is.Null);
            var found = !board.CanAdd(entity, new Coords(1, 1));
            Assert.That(found, Is.False);
            Assert.That(entity.IsDestroyed, Is.False);
        }
    }
}