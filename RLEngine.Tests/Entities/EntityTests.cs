using RLEngine.Core.Entities;
using RLEngine.Core.Utils;

using Xunit;
using FluentAssertions;
using System;

namespace RLEngine.Tests.Entities
{
    public class EntityTests
    {
        [Fact]
        public void EntityCreationPasses()
        {
            const string name = "Name";
            const bool isAgent = true;
            const int maxHealth = 100;
            const int speed = 80;
            const bool blocksGround = true;
            const bool blocksAir = false;
            const bool isGhost = false;

            // Arrange
            var entityType = new EntityType()
            {
                Name = name,
                IsAgent = isAgent,
                MaxHealth = maxHealth,
                Speed = speed,
                BlocksGround = blocksGround,
                BlocksAir = blocksAir,
                IsGhost = isGhost,
            };

            // Act
            var entity = new Entity(entityType);

            // Assert
            entity.Name.Should().Be(name);
            entity.IsAgent.Should().Be(isAgent);
            entity.MaxHealth.Should().Be(maxHealth);
            entity.Health.Should().Be(maxHealth);
            entity.MissingHealth.Should().Be(0);
            entity.Speed.Should().Be(speed);
            entity.BlocksGround.Should().Be(blocksGround);
            entity.BlocksAir.Should().Be(blocksAir);
            entity.IsGhost.Should().Be(isGhost);
            entity.Type.Should().Be(entityType);
            entity.IsActive.Should().BeFalse();
            entity.Position.Should().Be(Coords.MinusOne);
        }

        [Theory]
        [InlineData(-20)]
        [InlineData(60)]
        [InlineData(80)]
        [InlineData(1000)]
        public void EntityDamagePasses(int damage)
        {
            const int maxHealth = 80;

            // Arrange
            var entityType = new EntityType { MaxHealth = maxHealth };
            IEntity entity = new Entity(entityType);

            // Act
            var actualDamage = entity.Damage(damage);

            // Assert
            var expectedDamage = damage.Clamp(0, maxHealth);
            actualDamage.Should().Be(expectedDamage);
            entity.Health.Should().Be(maxHealth - expectedDamage);
            entity.MissingHealth.Should().Be(expectedDamage);
        }

        [Theory]
        [InlineData(-20)]
        [InlineData(60)]
        [InlineData(80)]
        [InlineData(1000)]
        public void EntityHealPasses(int healing)
        {
            const int maxHealth = 80;
            const int missingHealth = 30;

            // Arrange
            var entityType = new EntityType { MaxHealth = maxHealth };
            IEntity entity = new Entity(entityType);
            entity.Damage(missingHealth);

            // Act
            var actualHealing = entity.Heal(healing);

            // Assert
            var expectedHealing = healing.Clamp(0, missingHealth);
            actualHealing.Should().Be(expectedHealing);
            entity.Health.Should().Be(maxHealth - (missingHealth - expectedHealing));
            entity.MissingHealth.Should().Be(missingHealth - expectedHealing);
        }
    }
}