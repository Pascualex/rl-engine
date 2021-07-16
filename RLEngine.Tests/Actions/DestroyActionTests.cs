using RLEngine.Actions;
using RLEngine.Logs;
using RLEngine.State;
using RLEngine.Boards;
using RLEngine.Entities;
using RLEngine.Utils;
using RLEngine.Tests.Utils;

using NUnit.Framework;

namespace RLEngine.Tests.Actions
{
    [TestFixture]
    public class DestroyActionTests
    {
        [Test]
        public void DestroyPasses()
        {
            var f = new ContentFixture();

            var state = new GameState(new Size(3, 3), f.FloorTileType);

            var position = new Coords(1, 1);
            state.Spawn(f.GroundEntityType, position, out var entity);

            entity = entity.FailIfNull();
            var log = state.Destroy(entity);
            var destroyLog = (DestroyLog)log.FailIfNull();
            Assert.That(destroyLog.Entity, Is.SameAs(entity));

            var entityPosition = state.Board.GetCoords(entity);
            Assert.That(entityPosition, Is.Null);
        }

        [Test]
        public void DestroyFailsWhenEntityIsNotAdded()
        {
            var f = new ContentFixture();

            var state = new GameState(new Size(3, 3), f.FloorTileType);
            var entity = new Entity(f.GroundEntityType);

            var log = state.Destroy(entity);
            Assert.That(log, Is.Null);
        }
    }
}