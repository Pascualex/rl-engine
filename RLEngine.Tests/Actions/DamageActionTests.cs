using RLEngine.Core.Actions;
using RLEngine.Core.Turns;
using RLEngine.Core.Boards;
using RLEngine.Core.Entities;
using RLEngine.Core.Utils;
using RLEngine.Tests.Utils;

using NUnit.Framework;
using System;

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
            var turnManager = new TurnManager();
            var board = new Board(new Size(3, 3), f.FloorTileType);
            var executor = new ActionExecutor(turnManager, board);
            var position = new Coords(1, 1);
            executor.Spawn(f.UnparentedEntityType, position, out var entity);
            entity = entity.FailIfNull();
            var amount = new Amount { Base = damage };

            // Act
            var log = executor.Damage(entity, amount);

            // Assert
            var expectedDamage = damage.Clamp(0, entity.MaxHealth);
            log = log.FailIfNull();
            Assert.That(log.Target, Is.SameAs(entity));
            Assert.That(log.Attacker, Is.Null);
            Assert.That(log.Damage, Is.EqualTo(Math.Max(0, damage)));
            Assert.That(log.ActualDamage, Is.EqualTo(expectedDamage));
            Assert.That(entity.Health, Is.EqualTo(entity.MaxHealth - expectedDamage));
            Assert.That(entity.IsDestroyed, Is.False);
            var currentEntity = turnManager.Current;
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
            var turnManager = new TurnManager();
            var board = new Board(new Size(3, 3), f.FloorTileType);
            var executor = new ActionExecutor(turnManager, board);
            var position = new Coords(1, 1);
            executor.Spawn(f.UnparentedEntityType, position, out var entity);
            entity = entity.FailIfNull();
            var amount = new Amount { Base = damage };

            // Act
            var log = executor.Damage(entity, amount);

            // Assert
            var expectedDamage = damage.Clamp(0, entity.MaxHealth);
            log = log.FailIfNull();
            Assert.That(log.Target, Is.SameAs(entity));
            Assert.That(log.Attacker, Is.Null);
            Assert.That(log.Damage, Is.EqualTo(Math.Max(0, damage)));
            Assert.That(log.ActualDamage, Is.EqualTo(expectedDamage));
            Assert.That(entity.Health, Is.EqualTo(0));
            Assert.That(entity.IsDestroyed, Is.False);
        }
    }
}