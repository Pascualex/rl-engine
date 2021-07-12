using NUnit.Framework;

using RLEngine.Boards;
using RLEngine.Entities;
using RLEngine.Utils;
using RLEngine.Tests.Utils;

namespace RLEngine.Tests.Engine
{
    [TestFixture]
    public class BoardsTests
    {
        private class Fixture
        {
            public IEntityType GroundEntityType { get; }
            public IEntityType GhostAgentType { get; }
            public ITileType FloorTileType { get; }
            public ITileType WallTileType { get; }

            public Fixture()
            {
                GroundEntityType = new EntityType();
                GhostAgentType = new EntityType { IsGhost = true, IsAgent = true };
                FloorTileType = new TileType();
                WallTileType = new TileType { BlocksGround = true, BlocksAir = true };
            }
        }

        [Test]
        [TestCase(0, 0)]
        [TestCase(1, 1)]
        [TestCase(2, 4)]
        public void BoardCreatedPasses(int width, int height)
        {
            var f = new Fixture();

            var size = new Size(width, height);
            var board = new Board(size, f.FloorTileType);

            Assert.That(board.Size, Is.EqualTo(size));
            foreach (var tile in board.GetTiles())
                Assert.That(tile.FailIfNull().Type, Is.SameAs(f.FloorTileType));
        }

        [Test]
        [TestCase(0, 0)]
        [TestCase(0, 1)]
        [TestCase(2, 2)]
        public void AddPasses(int x, int y)
        {
            var f = new Fixture();

            var board = new Board(new Size(3, 3), f.FloorTileType);
            var entity = new Entity(f.GroundEntityType);

            var position = new Coords(x, y);
            var added = board.Add(entity, position);
            Assert.That(added, Is.True);

            var tile = board.GetTile(position).FailIfNull();
            Assert.That(tile.Entities, Has.Member(entity));
        }

        [Test]
        [TestCase(0, -1)]
        [TestCase(-1, -1)]
        [TestCase(3, 0)]
        public void AddFailsOutOfBounds(int x, int y)
        {
            var f = new Fixture();

            var board = new Board(new Size(3, 3), f.FloorTileType);
            var entity = new Entity(f.GroundEntityType);

            var position = new Coords(x, y);
            var added = board.Add(entity, position);
            Assert.That(added, Is.False);
        }

        [Test]
        public void AddPassesWithCompatibleEntity()
        {
            var f = new Fixture();

            var board = new Board(new Size(3, 3), f.FloorTileType);
            var entityA = new Entity(f.GroundEntityType);
            var entityB = new Entity(f.GhostAgentType);

            var position = new Coords(1, 1);
            board.Add(entityA, position);
            var added = board.Add(entityB, position);
            Assert.That(added, Is.True);

            var tile = board.GetTile(position).FailIfNull();
            Assert.That(tile.Entities, Has.Member(entityA));
            Assert.That(tile.Entities, Has.Member(entityB));
        }

        [Test]
        public void AddFailsWithIncompatibleEntity()
        {
            var f = new Fixture();

            var board = new Board(new Size(3, 3), f.FloorTileType);
            var entityA = new Entity(f.GroundEntityType);
            var entityB = new Entity(f.GroundEntityType);

            var position = new Coords(1, 1);
            board.Add(entityA, position);
            var added = board.Add(entityB, position);
            Assert.That(added, Is.False);

            var tile = board.GetTile(position).FailIfNull();
            Assert.That(tile.Entities, Has.Member(entityA));
            Assert.That(tile.Entities, Has.No.Member(entityB));
        }

        [Test]
        public void AddFailsWithIncompatibleTile()
        {
            var f = new Fixture();

            var board = new Board(new Size(3, 3), f.WallTileType);
            var entity = new Entity(f.GroundEntityType);

            var position = new Coords(1, 1);
            var added = board.Add(entity, position);
            Assert.That(added, Is.False);

            var tile = board.GetTile(position).FailIfNull();
            Assert.That(tile.Entities, Has.No.Member(entity));
        }

        [Test]
        [TestCase(0, 1, 2,  0, false)]
        [TestCase(0, 1, 2, -1, true)]
        [TestCase(0, 0, 2,  2, false)]
        [TestCase(0, 0, 2,  2,  true)]
        [TestCase(1, 1, 1,  1, false)]
        [TestCase(1, 1, 0,  0, true)]
        public void MovePasses(int ix, int iy, int fx, int fy, bool relative)
        {
            var f = new Fixture();

            var board = new Board(new Size(3, 3), f.FloorTileType);
            var entity = new Entity(f.GroundEntityType);

            var initialPosition = new Coords(ix, iy);
            board.Add(entity, initialPosition);

            var finalPosition = new Coords(fx, fy);
            var moved = board.Move(entity, finalPosition, relative);
            Assert.That(moved, Is.True);

            if (relative) finalPosition += initialPosition;
            if (initialPosition != finalPosition)
            {
                var initialTile = board.GetTile(initialPosition).FailIfNull();
                Assert.That(initialTile.Entities, Has.No.Member(entity));
            }

            var finalTile = board.GetTile(finalPosition).FailIfNull();
            Assert.That(finalTile.Entities, Has.Member(entity));
        }

        [Test]
        [TestCase(0, 0,  0, -1, false)]
        [TestCase(0, 0,  0, -1,  true)]
        [TestCase(1, 1, -1, -1, false)]
        [TestCase(1, 1, -2, -2,  true)]
        [TestCase(1, 2,  3,  0, false)]
        [TestCase(1, 2,  2,  0,  true)]
        public void MoveFailsOutOfBounds(int ix, int iy, int fx, int fy, bool relative)
        {
            var f = new Fixture();

            var board = new Board(new Size(3, 3), f.FloorTileType);
            var entity = new Entity(f.GroundEntityType);

            var initialPosition = new Coords(ix, iy);
            board.Add(entity, initialPosition);

            var finalPosition = new Coords(fx, fy);
            var moved = board.Move(entity, finalPosition, relative);
            Assert.That(moved, Is.False);

            var initialTile = board.GetTile(initialPosition).FailIfNull();
            Assert.That(initialTile.Entities, Has.Member(entity));
        }

        [Test]
        public void MoveFailsWhenEntityIsNotAdded()
        {
            var f = new Fixture();

            var board = new Board(new Size(3, 3), f.FloorTileType);
            var entity = new Entity(f.GroundEntityType);

            var finalPosition = new Coords(1, 1);
            var moved = board.Move(entity, finalPosition, false);
            Assert.That(moved, Is.False);

            var finalTile = board.GetTile(finalPosition).FailIfNull();
            Assert.That(finalTile.Entities, Has.No.Member(entity));
        }

        [Test]
        public void MovePassesWithCompatibleEntity()
        {
            var f = new Fixture();

            var board = new Board(new Size(3, 3), f.FloorTileType);
            var entityA = new Entity(f.GroundEntityType);
            var entityB = new Entity(f.GhostAgentType);

            var initialPosition = new Coords(0, 1);
            board.Add(entityA, initialPosition);

            var finalPosition = new Coords(2, 1);
            board.Add(entityB, finalPosition);
            var moved = board.Move(entityA, finalPosition, false);
            Assert.That(moved, Is.True);

            var initialTile = board.GetTile(initialPosition).FailIfNull();
            Assert.That(initialTile.Entities, Has.No.Member(entityA));

            var finalTile = board.GetTile(finalPosition).FailIfNull();
            Assert.That(finalTile.Entities, Has.Member(entityA));
            Assert.That(finalTile.Entities, Has.Member(entityB));
        }

        [Test]
        public void MoveFailsWithIncompatibleEntity()
        {
            var f = new Fixture();

            var board = new Board(new Size(3, 3), f.FloorTileType);
            var entityA = new Entity(f.GroundEntityType);
            var entityB = new Entity(f.GroundEntityType);

            var initialPosition = new Coords(0, 1);
            board.Add(entityA, initialPosition);

            var finalPosition = new Coords(2, 1);
            board.Add(entityB, finalPosition);
            var moved = board.Move(entityA, finalPosition, false);
            Assert.That(moved, Is.False);

            var initialTile = board.GetTile(initialPosition).FailIfNull();
            Assert.That(initialTile.Entities, Has.Member(entityA));

            var finalTile = board.GetTile(finalPosition).FailIfNull();
            Assert.That(finalTile.Entities, Has.No.Member(entityA));
            Assert.That(finalTile.Entities, Has.Member(entityB));
        }

        [Test]
        public void MoveFailsWithIncompatibleTile()
        {
            var f = new Fixture();

            var board = new Board(new Size(3, 3), f.FloorTileType);
            var entity = new Entity(f.GroundEntityType);

            var initialPosition = new Coords(0, 1);
            board.Add(entity, initialPosition);

            var finalPosition = new Coords(2, 1);
            board.ChangeTileType(f.WallTileType, finalPosition);
            var moved = board.Move(entity, finalPosition, false);
            Assert.That(moved, Is.False);

            var initialTile = board.GetTile(initialPosition).FailIfNull();
            Assert.That(initialTile.Entities, Has.Member(entity));

            var finalTile = board.GetTile(finalPosition).FailIfNull();
            Assert.That(finalTile.Entities, Has.No.Member(entity));
        }

        [Test]
        public void RemovePasses()
        {
            var f = new Fixture();

            var board = new Board(new Size(3, 3), f.FloorTileType);
            var entity = new Entity(f.GroundEntityType);

            var position = new Coords(1, 1);
            board.Add(entity, position);

            var removed = board.Remove(entity);
            Assert.That(removed, Is.True);

            var tile = board.GetTile(position).FailIfNull();
            Assert.That(tile.Entities, Has.No.Member(entity));
        }

        [Test]
        public void RemoveFailsWhenEntityIsNotAdded()
        {
            var f = new Fixture();

            var board = new Board(new Size(3, 3), f.FloorTileType);
            var entity = new Entity(f.GroundEntityType);

            var removed = board.Remove(entity);
            Assert.That(removed, Is.False);
        }

        [Test]
        [TestCase(0, 0)]
        [TestCase(0, 1)]
        [TestCase(2, 2)]
        public void ChangeTileTypePasses(int x, int y)
        {
            var f = new Fixture();

            var board = new Board(new Size(3, 3), f.FloorTileType);

            var position = new Coords(x, y);
            var changed = board.ChangeTileType(f.WallTileType, position);
            Assert.That(changed, Is.True);

            var tile = board.GetTile(position).FailIfNull();
            Assert.That(tile.Type, Is.SameAs(f.WallTileType));
        }

        [Test]
        [TestCase(0, -1)]
        [TestCase(-1, -1)]
        [TestCase(3, 0)]
        [TestCase(20, 30)]
        public void ChangeTileTypeFailsOutOfBounds(int x, int y)
        {
            var f = new Fixture();

            var board = new Board(new Size(3, 3), f.FloorTileType);

            var position = new Coords(x, y);
            var changed = board.ChangeTileType(f.WallTileType, position);
            Assert.That(changed, Is.False);
        }

        [Test]
        [TestCase(0, 0)]
        [TestCase(0, 1)]
        [TestCase(2, 2)]
        public void GetTilePasses(int x, int y)
        {
            var f = new Fixture();

            var board = new Board(new Size(3, 3), f.FloorTileType);

            var position = new Coords(x, y);
            var tile = board.GetTile(position);
            Assert.That(tile, Is.Not.Null);
        }

        [Test]
        [TestCase(0, -1)]
        [TestCase(-1, -1)]
        [TestCase(3, 0)]
        public void GetTileFailsOutOfBounds(int x, int y)
        {
            var f = new Fixture();

            var board = new Board(new Size(3, 3), f.FloorTileType);

            var position = new Coords(x, y);
            var tile = board.GetTile(position);
            Assert.That(tile, Is.Null);
        }
    }
}