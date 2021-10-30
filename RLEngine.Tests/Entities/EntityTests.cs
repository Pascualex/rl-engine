using RLEngine.Core.Entities;
using RLEngine.Tests.Utils;

using NUnit.Framework;
using System;

namespace RLEngine.Tests.Entities
{
    [TestFixture]
    public class EntityTests
    {
        [Test]
        public void InheritUnparentedEntityTypeAttributesPasses()
        {
            // Arrange
            var f = new ContentFixture();

            // Act
            var entity = new Entity(f.UnparentedEntityType);

            // Assert
            Assert.That(entity.Name, Is.EqualTo("Unparented entity type"));
            Assert.That(entity.IsAgent, Is.True);
            Assert.That(entity.MaxHealth, Is.EqualTo(80));
            Assert.That(entity.Health, Is.EqualTo(80));
            Assert.That(entity.MissingHealth, Is.EqualTo(0));
            Assert.That(entity.IsDestroyed, Is.EqualTo(false));
            Assert.That(entity.Speed, Is.EqualTo(120));
            Assert.That(entity.BlocksGround, Is.True);
            Assert.That(entity.BlocksAir, Is.False);
            Assert.That(entity.IsGhost, Is.False);
            Assert.That(entity.Type, Is.SameAs(f.UnparentedEntityType));
            Assert.That(entity.Type.Parent, Is.Null);
        }

        [Test]
        [TestCase(-20)]
        [TestCase(60)]
        [TestCase(80)]
        [TestCase(1000)]
        public void DamageEntityPasses(int damage)
        {
            // Arrange
            var f = new ContentFixture();
            var entity = new Entity(f.UnparentedEntityType);

            // Act
            var actualDamage = entity.Damage(damage);

            // Assert
            var expectedDamage = damage.Clamp(0, entity.MaxHealth);
            Assert.That(actualDamage, Is.EqualTo(expectedDamage));
            Assert.That(entity.Health, Is.EqualTo(entity.MaxHealth - expectedDamage));
            Assert.That(entity.MissingHealth, Is.EqualTo(expectedDamage));
            Assert.That(entity.IsDestroyed, Is.EqualTo(false));
        }
    }
}