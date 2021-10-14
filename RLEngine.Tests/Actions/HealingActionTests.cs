using RLEngine.Actions;
using RLEngine.Logs;
using RLEngine.State;
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
            var state = new GameState(new Size(3, 3), f.FloorTileType);
            var position = new Coords(1, 1);
            state.Spawn(f.UnparentedEntityType, position, out var entity);
            entity = entity.FailIfNull();
            var damageAmount = new ActionAmount { Base = damage };
            state.Damage(entity, damageAmount);
            var healingAmount = new ActionAmount { Base = healing };

            // Act
            var log = state.Heal(entity, healingAmount);

            // Assert
            var expectedHealing = healing.Clamp(0, damage);
            var healingLog = (HealingLog)log.FailIfNull();
            Assert.That(healingLog.Target, Is.SameAs(entity));
            Assert.That(healingLog.Healer, Is.Null);
            Assert.That(healingLog.Healing, Is.EqualTo(Math.Max(0, healing)));
            Assert.That(healingLog.ActualHealing, Is.EqualTo(expectedHealing));
            Assert.That(entity.Health, Is.EqualTo(entity.MaxHealth - damage + expectedHealing));
        }
    }
}