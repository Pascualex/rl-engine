using RLEngine.Actions;
using RLEngine.Logs;
using RLEngine.Turns;
using RLEngine.Boards;
using RLEngine.Entities;
using RLEngine.Utils;
using RLEngine.Tests.Utils;

using NUnit.Framework;
using System;

namespace RLEngine.Tests.Actions
{
    [TestFixture]
    public class HealingActionTests
    {
        [Test]
        [TestCase(40, -20)]
        [TestCase(40, 30)]
        [TestCase(40, 50)]
        public void HealPasses(int damage, int healing)
        {
            // Arrange
            var f = new ContentFixture();
            var turnManager = new TurnManager();
            var board = new Board(new Size(3, 3), f.FloorTileType);
            var executor = new ActionExecutor(turnManager, board);
            var position = new Coords(1, 1);
            executor.Spawn(f.UnparentedEntityType, position, out var entity);
            entity = entity.FailIfNull();
            var damageAmount = new Amount { Base = damage };
            executor.Damage(entity, damageAmount);
            var healingAmount = new Amount { Base = healing };

            // Act
            var log = executor.Heal(entity, healingAmount);

            // Assert
            var expectedHealing = healing.Clamp(0, damage);
            log = log.FailIfNull();
            Assert.That(log.Target, Is.SameAs(entity));
            Assert.That(log.Healer, Is.Null);
            Assert.That(log.Healing, Is.EqualTo(Math.Max(0, healing)));
            Assert.That(log.ActualHealing, Is.EqualTo(expectedHealing));
            Assert.That(entity.Health, Is.EqualTo(entity.MaxHealth - damage + expectedHealing));
        }
    }
}