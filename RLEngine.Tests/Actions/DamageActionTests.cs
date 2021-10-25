using RLEngine.Events;
using RLEngine.Logs;
using RLEngine.Turns;
using RLEngine.Boards;
using RLEngine.Utils;
using RLEngine.Tests.Utils;

using NUnit.Framework;
using System;
using System.Linq;

namespace RLEngine.Tests.Actions
{
    [TestFixture]
    public class DamageActionTests
    {
        [Test]
        [TestCase(-20)]
        [TestCase(0)]
        [TestCase(79)]
        public void DamagePassesWithoutDeath(int damage)
        {
            // Arrange
            var f = new ContentFixture();
            var board = new Board(new Size(3, 3), f.FloorTileType);
            var ctx = new EventContext(new EventQueue(), new TurnManager(), board);
            var position = new Coords(1, 1);
            ctx.Spawn(f.UnparentedEntityType, position, out var entity);
            entity = entity.FailIfNull();
            var amount = new ActionAmount { Base = damage };

            // Act
            var log = ctx.Damage(entity, amount);

            // Assert
            var expectedDamage = damage.Clamp(0, entity.MaxHealth);
            var damageLog = (DamageLog)log.FailIfNull();
            Assert.That(damageLog.Target, Is.SameAs(entity));
            Assert.That(damageLog.Attacker, Is.Null);
            Assert.That(damageLog.Damage, Is.EqualTo(Math.Max(0, damage)));
            Assert.That(damageLog.ActualDamage, Is.EqualTo(expectedDamage));
            Assert.That(entity.Health, Is.EqualTo(entity.MaxHealth - expectedDamage));
            Assert.That(entity.IsDestroyed, Is.False);
            var currentEntity = ctx.TurnManager.Current;
            Assert.That(currentEntity, Is.SameAs(entity));
            Assert.That(entity.Position, Is.EqualTo(position));
        }

        [Test]
        [TestCase(80)]
        [TestCase(1000)]
        public void DamagePassesWithDeath(int damage)
        {
            // Arrange
            var f = new ContentFixture();
            var board = new Board(new Size(3, 3), f.FloorTileType);
            var ctx = new EventContext(new EventQueue(), new TurnManager(), board);
            var position = new Coords(1, 1);
            ctx.Spawn(f.UnparentedEntityType, position, out var entity);
            entity = entity.FailIfNull();
            var amount = new ActionAmount { Base = damage };

            // Act
            var log = ctx.Damage(entity, amount);

            // Assert
            var expectedDamage = damage.Clamp(0, entity.MaxHealth);
            var damageLog = (DamageLog)log.FailIfNull();
            Assert.That(damageLog.Target, Is.SameAs(entity));
            Assert.That(damageLog.Attacker, Is.Null);
            Assert.That(damageLog.Damage, Is.EqualTo(Math.Max(0, damage)));
            Assert.That(damageLog.ActualDamage, Is.EqualTo(expectedDamage));
            Assert.That(entity.Health, Is.EqualTo(0));
            Assert.That(entity.IsDestroyed, Is.False);
        }
    }
}