using NUnit.Framework;
using NSubstitute;

using RLEngine.Entities;

namespace RLEngine.Tests.Entities
{
    [TestFixture]
    public class EntityTests
    {
        private class Fixture
        {
            public IEntityType UnparentedEntityType { get; }

            public Fixture()
            {
                UnparentedEntityType = Substitute.For<IEntityType>();
                UnparentedEntityType.Name.Returns("Unparented entity type");
                UnparentedEntityType.IsAgent.Returns(true);
                UnparentedEntityType.Speed.Returns(120);
                UnparentedEntityType.BlocksGround.Returns(true);
                UnparentedEntityType.Parent.Returns((IEntityType?)null);
            }
        }

        [Test]
        public void InheritUnparentedEntityTypeAttributesPasses()
        {
            var f = new Fixture();

            var entity = new Entity(f.UnparentedEntityType);
            Assert.That(entity.Name, Is.EqualTo("Unparented entity type"));
            Assert.That(entity.IsAgent, Is.True);
            Assert.That(entity.Speed, Is.EqualTo(120));
            Assert.That(entity.BlocksGround, Is.True);
            Assert.That(entity.BlocksAir, Is.False);
            Assert.That(entity.IsGhost, Is.False);
            Assert.That(entity.Visuals, Is.Null);
            Assert.That(entity.Type, Is.EqualTo(f.UnparentedEntityType));
            Assert.That(entity.Type.Parent, Is.Null);
        }
    }
}