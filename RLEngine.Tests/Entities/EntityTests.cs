using RLEngine.Entities;
using RLEngine.Tests.Utils;

using NUnit.Framework;

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
            Assert.That(entity.Speed, Is.EqualTo(120));
            Assert.That(entity.BlocksGround, Is.True);
            Assert.That(entity.BlocksAir, Is.False);
            Assert.That(entity.IsGhost, Is.False);
            Assert.That(entity.Visuals, Is.Null);
            Assert.That(entity.Type, Is.SameAs(f.UnparentedEntityType));
            Assert.That(entity.Type.Parent, Is.Null);
        }
    }
}