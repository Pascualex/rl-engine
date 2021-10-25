using RLEngine.Events;
using RLEngine.Logs;
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
            var board = new Board(new Size(3, 3), f.FloorTileType);
            var ctx = new EventContext(new EventQueue(), new TurnManager(), board);
            var position = new Coords(1, 1);
            ctx.Spawn(f.AgentType, position, out var entity);
            entity = entity.FailIfNull();

            // Act
            var log = ctx.Destroy(entity);

            // Assert
            var destructionLog = (DestructionLog)log.FailIfNull();
            Assert.That(destructionLog.Entity, Is.SameAs(entity));
            var currentEntity = ctx.TurnManager.Current;
            Assert.That(currentEntity, Is.Null);
            var found = !ctx.Board.CanAdd(entity, position);
            Assert.That(found, Is.False);
            Assert.That(entity.IsDestroyed, Is.True);
        }

        [Test]
        public void DestroyFailsWhenEntityIsNotAdded()
        {
            // Arrange
            var f = new ContentFixture();
            var board = new Board(new Size(3, 3), f.FloorTileType);
            var ctx = new EventContext(new EventQueue(), new TurnManager(), board);
            var entity = new Entity(f.AgentType);

            // Act
            var log = ctx.Destroy(entity);

            // Assert
            Assert.That(log, Is.Null);
            var currentEntity = ctx.TurnManager.Current;
            Assert.That(currentEntity, Is.Null);
            var found = !ctx.Board.CanAdd(entity, new Coords(1, 1));
            Assert.That(found, Is.False);
            Assert.That(entity.IsDestroyed, Is.False);
        }
    }
}