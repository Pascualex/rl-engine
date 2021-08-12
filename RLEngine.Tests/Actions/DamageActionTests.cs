using RLEngine.Actions;
using RLEngine.Logs;
using RLEngine.State;
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
        public void DamagePassesWithNoDeath(int damage)
        {
            // Arrange
            var f = new ContentFixture();
            var state = new GameState(new Size(3, 3), f.FloorTileType);
            var position = new Coords(1, 1);
            state.Spawn(f.UnparentedEntityType, position, out var entity);
            entity = entity.FailIfNull();
            var amount = new ActionAmount { Base = damage };

            // Act
            var log = state.Damage(entity, amount);

            // Assert
            var expectedDamage = damage.Clamp(0, entity.MaxHealth);
            var damageLog = (DamageLog)log.FailIfNull();
            Assert.That(damageLog.Target, Is.SameAs(entity));
            Assert.That(damageLog.Attacker, Is.Null);
            Assert.That(damageLog.Damage, Is.EqualTo(Math.Max(0, damage)));
            Assert.That(damageLog.ActualDamage, Is.EqualTo(expectedDamage));
            Assert.That(entity.Health, Is.EqualTo(entity.MaxHealth - expectedDamage));
            Assert.That(entity.IsDead, Is.False);
            var currentEntity = state.TurnManager.Current;
            Assert.That(currentEntity, Is.SameAs(entity));
            var found = state.Board.TryGetCoords(entity, out var entityPosition);
            Assert.That(found, Is.True);
            Assert.That(entityPosition, Is.EqualTo(position));
        }

        [Test]
        [TestCase(80)]
        [TestCase(1000)]
        public void DamagePassesWithDeath(int damage)
        {
            // Arrange
            var f = new ContentFixture();
            var state = new GameState(new Size(3, 3), f.FloorTileType);
            var position = new Coords(1, 1);
            state.Spawn(f.UnparentedEntityType, position, out var entity);
            entity = entity.FailIfNull();
            var amount = new ActionAmount { Base = damage };

            // Act
            var log = state.Damage(entity, amount);

            // Assert
            var expectedDamage = damage.Clamp(0, entity.MaxHealth);
            var combinedLog = (CombinedLog)log.FailIfNull();
            var damageLog = (DamageLog)combinedLog.Logs.ElementAt(0);
            Assert.That(damageLog.Target, Is.SameAs(entity));
            Assert.That(damageLog.Attacker, Is.Null);
            Assert.That(damageLog.Damage, Is.EqualTo(Math.Max(0, damage)));
            Assert.That(damageLog.ActualDamage, Is.EqualTo(expectedDamage));
            Assert.That(entity.Health, Is.EqualTo(entity.MaxHealth - expectedDamage));
            Assert.That(entity.IsDead, Is.True);
            var destroyLog = (DestroyLog)combinedLog.Logs.ElementAt(1);
            Assert.That(destroyLog.Entity, Is.SameAs(entity));
            var currentEntity = state.TurnManager.Current;
            Assert.That(currentEntity, Is.Null);
            var found = state.Board.TryGetCoords(entity, out _);
            Assert.That(found, Is.False);
        }
    }
}