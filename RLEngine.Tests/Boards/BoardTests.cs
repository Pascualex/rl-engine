using RLEngine.Boards;
using RLEngine.Entities;
using RLEngine.Utils;
using RLEngine.Tests.Utils;

using NUnit.Framework;

namespace RLEngine.Tests.Boards
{
    [TestFixture]
    public class BoardsTests
    {
        [Test]
        [TestCase(0, 0)]
        [TestCase(1, 1)]
        [TestCase(2, 4)]
        public void BoardCreatedPasses(int width, int height)
        {
            // Arrange
            var f = new ContentFixture();
            var size = new Size(width, height);

            // Act
            var board = new Board(size, f.FloorTileType);

            // Assert
            Assert.That(board.Size, Is.EqualTo(size));
            for (var i = 0; i < board.Size.Y; i++)
            {
                for (var j = 0; j < board.Size.X; j++)
                {
                    var tileType = board.GetTileType(new Coords(j, i)).FailIfNull();
                    Assert.That(tileType, Is.SameAs(f.FloorTileType));
                }
            }
        }

        [Test]
        [TestCase(0, 0)]
        [TestCase(0, 1)]
        [TestCase(2, 2)]
        public void AddPasses(int x, int y)
        {
            // Arrange
            var f = new ContentFixture();
            var board = new Board(new Size(3, 3), f.FloorTileType);
            var entity = new Entity(f.GroundEntityType);
            var position = new Coords(x, y);

            // Act
            var added = board.Add(entity, position);

            // Assert
            Assert.That(added, Is.True);
            var found = board.TryGetCoords(entity, out var entityPosition);
            Assert.That(found, Is.True);
            Assert.That(entityPosition, Is.EqualTo(position));
            var entities = board.GetEntities(position);
            Assert.That(entities, Has.Member(entity));
        }

        [Test]
        [TestCase(0, -1)]
        [TestCase(-1, -1)]
        [TestCase(3, 0)]
        public void AddFailsOutOfBounds(int x, int y)
        {
            // Arrange
            var f = new ContentFixture();
            var board = new Board(new Size(3, 3), f.FloorTileType);
            var entity = new Entity(f.GroundEntityType);
            var position = new Coords(x, y);

            // Act
            var added = board.Add(entity, position);

            // Assert
            Assert.That(added, Is.False);
            var found = board.TryGetCoords(entity, out _);
            Assert.That(found, Is.False);
            var entities = board.GetEntities(position);
            Assert.That(entities, Has.No.Member(entity));
        }

        [Test]
        public void AddPassesWithCompatibleEntity()
        {
            // Arrange
            var f = new ContentFixture();
            var board = new Board(new Size(3, 3), f.FloorTileType);
            var entityA = new Entity(f.GroundEntityType);
            var entityB = new Entity(f.GhostAgentType);
            var position = new Coords(1, 1);
            board.Add(entityA, position);

            // Act
            var added = board.Add(entityB, position);

            // Assert
            Assert.That(added, Is.True);
            var entityAFound = board.TryGetCoords(entityA, out var entityAPosition);
            Assert.That(entityAFound, Is.True);
            Assert.That(entityAPosition, Is.EqualTo(position));
            var entityBFound = board.TryGetCoords(entityB, out var entityBPosition);
            Assert.That(entityBFound, Is.True);
            Assert.That(entityBPosition, Is.EqualTo(position));
            var entities = board.GetEntities(position);
            Assert.That(entities, Has.Member(entityA));
            Assert.That(entities, Has.Member(entityB));
        }

        [Test]
        public void AddFailsWithIncompatibleEntity()
        {
            // Arrange
            var f = new ContentFixture();
            var board = new Board(new Size(3, 3), f.FloorTileType);
            var entityA = new Entity(f.GroundEntityType);
            var entityB = new Entity(f.GroundEntityType);
            var position = new Coords(1, 1);
            board.Add(entityA, position);

            // Act
            var added = board.Add(entityB, position);

            // Assert
            Assert.That(added, Is.False);
            var entityAFound = board.TryGetCoords(entityA, out var entityAPosition);
            Assert.That(entityAFound, Is.True);
            Assert.That(entityAPosition, Is.EqualTo(position));
            var entityBFound = board.TryGetCoords(entityB, out _);
            Assert.That(entityBFound, Is.False);
            var entities = board.GetEntities(position);
            Assert.That(entities, Has.Member(entityA));
            Assert.That(entities, Has.No.Member(entityB));
        }

        [Test]
        public void AddFailsWithIncompatibleTile()
        {
            // Arrange
            var f = new ContentFixture();
            var board = new Board(new Size(3, 3), f.WallTileType);
            var entity = new Entity(f.GroundEntityType);
            var position = new Coords(1, 1);

            // Act
            var added = board.Add(entity, position);

            // Assert
            Assert.That(added, Is.False);
            var found = board.TryGetCoords(entity, out _);
            Assert.That(found, Is.False);
            var entities = board.GetEntities(position);
            Assert.That(entities, Has.No.Member(entity));
        }

        [Test]
        [TestCase(0, 1, 2, 0)]
        [TestCase(0, 0, 2, 2)]
        [TestCase(1, 1, 1, 1)]
        public void MovePasses(int ix, int iy, int fx, int fy)
        {
            // Arrange
            var f = new ContentFixture();
            var board = new Board(new Size(3, 3), f.FloorTileType);
            var entity = new Entity(f.GroundEntityType);
            var initialPosition = new Coords(ix, iy);
            var finalPosition = new Coords(fx, fy);
            board.Add(entity, initialPosition);

            // Act
            var moved = board.Move(entity, finalPosition);

            // Assert
            Assert.That(moved, Is.True);
            var found = board.TryGetCoords(entity, out var entityPosition);
            Assert.That(found, Is.True);
            Assert.That(entityPosition, Is.EqualTo(finalPosition));
            if (initialPosition != finalPosition)
            {
                var initialEntities = board.GetEntities(initialPosition);
                Assert.That(initialEntities, Has.No.Member(entity));
            }
            var finalEntities = board.GetEntities(finalPosition);
            Assert.That(finalEntities, Has.Member(entity));
        }

        [Test]
        [TestCase(0, 0,  0, -1)]
        [TestCase(1, 1, -1, -1)]
        [TestCase(1, 2,  3,  0)]
        public void MoveFailsOutOfBounds(int ix, int iy, int fx, int fy)
        {
            // Arrange
            var f = new ContentFixture();
            var board = new Board(new Size(3, 3), f.FloorTileType);
            var entity = new Entity(f.GroundEntityType);
            var initialPosition = new Coords(ix, iy);
            var finalPosition = new Coords(fx, fy);
            board.Add(entity, initialPosition);

            // Act
            var moved = board.Move(entity, finalPosition);

            // Assert
            Assert.That(moved, Is.False);
            var found = board.TryGetCoords(entity, out var entityPosition);
            Assert.That(found, Is.True);
            Assert.That(entityPosition, Is.EqualTo(initialPosition));
            var initialEntities = board.GetEntities(initialPosition);
            Assert.That(initialEntities, Has.Member(entity));
            var finalEntities = board.GetEntities(finalPosition);
            Assert.That(finalEntities, Has.No.Member(entity));
        }

        [Test]
        public void MoveFailsWhenEntityIsNotAdded()
        {
            // Arrange
            var f = new ContentFixture();
            var board = new Board(new Size(3, 3), f.FloorTileType);
            var entity = new Entity(f.GroundEntityType);
            var finalPosition = new Coords(1, 1);

            // Act
            var moved = board.Move(entity, finalPosition);

            // Assert
            Assert.That(moved, Is.False);
            var found = board.TryGetCoords(entity, out _);
            Assert.That(found, Is.False);
            var finalEntities = board.GetEntities(finalPosition);
            Assert.That(finalEntities, Has.No.Member(entity));
        }

        [Test]
        public void MovePassesWithCompatibleEntity()
        {
            // Arrange
            var f = new ContentFixture();
            var board = new Board(new Size(3, 3), f.FloorTileType);
            var entityA = new Entity(f.GroundEntityType);
            var entityB = new Entity(f.GhostAgentType);
            var initialPosition = new Coords(0, 1);
            var finalPosition = new Coords(2, 1);
            board.Add(entityA, finalPosition);
            board.Add(entityB, initialPosition);

            // Act
            var moved = board.Move(entityB, finalPosition);

            // Assert
            Assert.That(moved, Is.True);
            var entityAFound = board.TryGetCoords(entityA, out var entityAPosition);
            Assert.That(entityAFound, Is.True);
            Assert.That(entityAPosition, Is.EqualTo(finalPosition));
            var entityBFound = board.TryGetCoords(entityB, out var entityBPosition);
            Assert.That(entityBFound, Is.True);
            Assert.That(entityBPosition, Is.EqualTo(finalPosition));
            var initialEntities = board.GetEntities(initialPosition);
            Assert.That(initialEntities, Has.No.Member(entityB));
            var finalEntities = board.GetEntities(finalPosition);
            Assert.That(finalEntities, Has.Member(entityA));
            Assert.That(finalEntities, Has.Member(entityB));
        }

        [Test]
        public void MoveFailsWithIncompatibleEntity()
        {
            // Arrange
            var f = new ContentFixture();
            var board = new Board(new Size(3, 3), f.FloorTileType);
            var entityA = new Entity(f.GroundEntityType);
            var entityB = new Entity(f.GroundEntityType);
            var initialPosition = new Coords(0, 1);
            var finalPosition = new Coords(2, 1);
            board.Add(entityA, finalPosition);
            board.Add(entityB, initialPosition);

            // Act
            var moved = board.Move(entityB, finalPosition);

            // Assert
            Assert.That(moved, Is.False);
            var entityAFound = board.TryGetCoords(entityA, out var entityAPosition);
            Assert.That(entityAFound, Is.True);
            Assert.That(entityAPosition, Is.EqualTo(finalPosition));
            var entityBFound = board.TryGetCoords(entityB, out var entityBPosition);
            Assert.That(entityBFound, Is.True);
            Assert.That(entityBPosition, Is.EqualTo(initialPosition));
            var initialEntities = board.GetEntities(initialPosition);
            Assert.That(initialEntities, Has.Member(entityB));
            var finalEntities = board.GetEntities(finalPosition);
            Assert.That(finalEntities, Has.Member(entityA));
            Assert.That(finalEntities, Has.No.Member(entityB));
        }

        [Test]
        public void MoveFailsWithIncompatibleTile()
        {
            // Arrange
            var f = new ContentFixture();
            var board = new Board(new Size(3, 3), f.FloorTileType);
            var entity = new Entity(f.GroundEntityType);
            var initialPosition = new Coords(0, 1);
            var finalPosition = new Coords(2, 1);
            board.Add(entity, initialPosition);
            board.Modify(f.WallTileType, finalPosition);

            // Act
            var moved = board.Move(entity, finalPosition);

            // Assert
            Assert.That(moved, Is.False);
            var found = board.TryGetCoords(entity, out var entityPosition);
            Assert.That(found, Is.True);
            Assert.That(entityPosition, Is.EqualTo(initialPosition));
            var initialEntities = board.GetEntities(initialPosition);
            Assert.That(initialEntities, Has.Member(entity));
            var finalEntities = board.GetEntities(finalPosition);
            Assert.That(finalEntities, Has.No.Member(entity));
        }

        [Test]
        public void RemovePasses()
        {
            // Arrange
            var f = new ContentFixture();
            var board = new Board(new Size(3, 3), f.FloorTileType);
            var entity = new Entity(f.GroundEntityType);
            var position = new Coords(1, 1);
            board.Add(entity, position);

            // Act
            var removed = board.Remove(entity);

            // Assert
            Assert.That(removed, Is.True);
            var found = board.TryGetCoords(entity, out _);
            Assert.That(found, Is.False);
            var entities = board.GetEntities(position);
            Assert.That(entities, Has.No.Member(entity));
        }

        [Test]
        public void RemoveFailsWhenEntityIsNotAdded()
        {
            // Arrange
            var f = new ContentFixture();
            var board = new Board(new Size(3, 3), f.FloorTileType);
            var entity = new Entity(f.GroundEntityType);

            // Act
            var removed = board.Remove(entity);

            // Assert
            Assert.That(removed, Is.False);
            var found = board.TryGetCoords(entity, out _);
            Assert.That(found, Is.False);
        }

        [Test]
        [TestCase(0, 0)]
        [TestCase(0, 1)]
        [TestCase(2, 2)]
        public void ModifyPasses(int x, int y)
        {
            // Arrange
            var f = new ContentFixture();
            var board = new Board(new Size(3, 3), f.FloorTileType);
            var position = new Coords(x, y);

            // Act
            var changed = board.Modify(f.WallTileType, position);

            // Assert
            Assert.That(changed, Is.True);
            var tileType = board.GetTileType(position);
            Assert.That(tileType, Is.SameAs(f.WallTileType));
        }

        [Test]
        [TestCase(0, -1)]
        [TestCase(-1, -1)]
        [TestCase(3, 0)]
        [TestCase(20, 30)]
        public void ModifyFailsOutOfBounds(int x, int y)
        {
            // Arrange
            var f = new ContentFixture();
            var board = new Board(new Size(3, 3), f.FloorTileType);
            var position = new Coords(x, y);

            // Act
            var changed = board.Modify(f.WallTileType, position);

            // Assert
            Assert.That(changed, Is.False);
            var tileType = board.GetTileType(position);
            Assert.That(tileType, Is.Null);
        }
    }
}