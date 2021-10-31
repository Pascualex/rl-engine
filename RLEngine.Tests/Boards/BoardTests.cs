using RLEngine.Core.Boards;
using RLEngine.Core.Entities;
using RLEngine.Core.Utils;

using Xunit;
using FluentAssertions;
using NSubstitute;

namespace RLEngine.Tests.Boards
{
    public class BoardsTests
    {
        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 1)]
        [InlineData(2, 4)]
        public void BoardInitializationPasses(int width, int height)
        {
            // Arrange
            var size = new Size(width, height);
            var defaultTileType = new TileType();

            // Act
            var board = new Board(size, defaultTileType);

            // Assert
            board.Size.Should().Be(size);
            for (var i = 0; i < board.Size.Y; i++)
            {
                for (var j = 0; j < board.Size.X; j++)
                {
                    board.GetTileType(new Coords(j, i)).Should().Be(defaultTileType);
                }
            }
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(0, 1)]
        [InlineData(2, 2)]
        public void BoardAddPasses(int x, int y)
        {
            var size = new Size(3, 3);
            var position = new Coords(x, y);

            // Arrange
            var entity = Substitute.For<IEntity>();
            entity.BlocksGround.Returns(true);
            var defaultTileType = new TileType { BlocksGround = false };
            var board = new Board(size, defaultTileType);

            // Act
            var added = board.Add(entity, position);

            // Assert
            added.Should().BeTrue();
            board.GetEntities(position).Should().Equal(entity);
            entity.Received().OnSpawn(position);
        }

        [Theory]
        [InlineData(0, -1)]
        [InlineData(-1, -1)]
        [InlineData(3, 0)]
        public void BoardAddFailsWhenOutOfBounds(int x, int y)
        {
            var size = new Size(3, 3);
            var position = new Coords(x, y);

            // Arrange
            var entity = Substitute.For<IEntity>();
            var defaultTileType = new TileType { BlocksGround = false };
            var board = new Board(size, defaultTileType);

            // Act
            var added = board.Add(entity, position);

            // Assert
            added.Should().BeFalse();
            board.GetEntities(position).Should().BeEmpty();
            entity.DidNotReceive().OnSpawn(position);
        }

        [Fact]
        public void BoardAddPassesWithCompatibleEntity()
        {
            var size = new Size(3, 3);
            var position = new Coords(1, 1);

            // Arrange
            var entityA = Substitute.For<IEntity>();
            entityA.BlocksGround.Returns(true);
            entityA.IsAgent.Returns(false);
            entityA.IsGhost.Returns(false);
            var entityB = Substitute.For<IEntity>();
            entityB.BlocksGround.Returns(true);
            entityB.IsAgent.Returns(true);
            entityB.IsGhost.Returns(true);
            var defaultTileType = new TileType { BlocksGround = false };
            var board = new Board(size, defaultTileType);
            board.Add(entityA, position);
            entityA.ClearReceivedCalls();

            // Act
            var added = board.Add(entityB, position);

            // Assert
            added.Should().BeTrue();
            board.GetEntities(position).Should().Equal(entityA, entityB);
            entityA.DidNotReceive().OnSpawn(Arg.Any<Coords>());
            entityB.Received().OnSpawn(position);
        }

        [Fact]
        public void BoardAddFailsWithIncompatibleEntity()
        {
            var size = new Size(3, 3);
            var position = new Coords(1, 1);

            // Arrange
            var entityA = Substitute.For<IEntity>();
            entityA.BlocksGround.Returns(true);
            var entityB = Substitute.For<IEntity>();
            entityB.BlocksGround.Returns(true);
            var defaultTileType = new TileType { BlocksGround = false };
            var board = new Board(size, defaultTileType);
            board.Add(entityA, position);

            // Act
            var added = board.Add(entityB, position);

            // Assert
            added.Should().BeFalse();
            board.GetEntities(position).Should().Equal(entityA);
            entityA.Received().OnSpawn(position);
            entityB.DidNotReceive().OnSpawn(position);
        }

        [Fact]
        public void BoardAddFailsWithIncompatibleTile()
        {
            var size = new Size(3, 3);
            var position = new Coords(1, 1);

            // Arrange
            var entity = Substitute.For<IEntity>();
            entity.BlocksGround.Returns(true);
            var defaultTileType = new TileType { BlocksGround = true };
            var board = new Board(size, defaultTileType);

            // Act
            var added = board.Add(entity, position);

            // Assert
            added.Should().BeFalse();
            board.GetEntities(position).Should().BeEmpty();
            entity.DidNotReceive().OnSpawn(position);
        }

        [Theory]
        [InlineData(0, 1, 2, 0)]
        [InlineData(0, 0, 2, 2)]
        public void BoardMovePasses(int ix, int iy, int fx, int fy)
        {
            var size = new Size(3, 3);
            var initialPosition = new Coords(ix, iy);
            var finalPosition = new Coords(fx, fy);

            // Arrange
            var entity = Substitute.For<IEntity>();
            entity.BlocksGround.Returns(true);
            var defaultTileType = new TileType { BlocksGround = false };
            var board = new Board(size, defaultTileType);
            board.Add(entity, initialPosition);
            entity.Position.Returns(initialPosition);
            entity.ClearReceivedCalls();

            // Act
            var moved = board.Move(entity, finalPosition);

            // Assert
            moved.Should().BeTrue();
            board.GetEntities(initialPosition).Should().BeEmpty();
            board.GetEntities(finalPosition).Should().Equal(entity);
            entity.Received().OnMove(finalPosition);
        }

        [Theory]
        [InlineData(0, 0,  0, -1)]
        [InlineData(1, 1, -1, -1)]
        [InlineData(1, 2,  3,  0)]
        public void BoardMoveFailsWhenOutOfBounds(int ix, int iy, int fx, int fy)
        {
            var size = new Size(3, 3);
            var initialPosition = new Coords(ix, iy);
            var finalPosition = new Coords(fx, fy);

            // Arrange
            var entity = Substitute.For<IEntity>();
            entity.BlocksGround.Returns(true);
            var defaultTileType = new TileType { BlocksGround = false };
            var board = new Board(size, defaultTileType);
            board.Add(entity, initialPosition);
            entity.Position.Returns(initialPosition);
            entity.ClearReceivedCalls();

            // Act
            var moved = board.Move(entity, finalPosition);

            // Assert
            moved.Should().BeFalse();
            board.GetEntities(initialPosition).Should().Equal(entity);
            board.GetEntities(finalPosition).Should().BeEmpty();
            entity.DidNotReceive().OnMove(Arg.Any<Coords>());
        }

        [Fact]
        public void BoardMoveFailsWhenEntityIsNotAdded()
        {
            var size = new Size(3, 3);
            var finalPosition = new Coords(1, 1);

            // Arrange
            var entity = Substitute.For<IEntity>();
            var defaultTileType = new TileType { BlocksGround = false };
            var board = new Board(size, defaultTileType);

            // Act
            var moved = board.Move(entity, finalPosition);

            // Assert
            moved.Should().BeFalse();
            board.GetEntities(finalPosition).Should().BeEmpty();
            entity.DidNotReceive().OnMove(Arg.Any<Coords>());
        }

        [Fact]
        public void BoardMovePassesWithCompatibleEntity()
        {
            var size = new Size(3, 3);
            var initialPosition = new Coords(0, 1);
            var finalPosition = new Coords(2, 1);

            // Arrange
            var entityA = Substitute.For<IEntity>();
            entityA.BlocksGround.Returns(true);
            entityA.IsGhost.Returns(false);
            entityA.IsAgent.Returns(false);
            var entityB = Substitute.For<IEntity>();
            entityB.BlocksGround.Returns(true);
            entityB.IsGhost.Returns(true);
            entityB.IsAgent.Returns(true);
            var defaultTileType = new TileType { BlocksGround = false };
            var board = new Board(size, defaultTileType);
            board.Add(entityA, finalPosition);
            board.Add(entityB, initialPosition);
            entityB.Position.Returns(initialPosition);
            entityA.ClearReceivedCalls();
            entityB.ClearReceivedCalls();

            // Act
            var moved = board.Move(entityB, finalPosition);

            // Assert
            moved.Should().BeTrue();
            board.GetEntities(initialPosition).Should().BeEmpty();
            board.GetEntities(finalPosition).Should().Equal(entityA, entityB);
            entityA.DidNotReceive().OnMove(Arg.Any<Coords>());
            entityB.Received().OnMove(finalPosition);
        }

        [Fact]
        public void BoardMoveFailsWithIncompatibleEntity()
        {
            var size = new Size(3, 3);
            var initialPosition = new Coords(0, 1);
            var finalPosition = new Coords(2, 1);

            // Arrange
            var entityA = Substitute.For<IEntity>();
            entityA.BlocksGround.Returns(true);
            var entityB = Substitute.For<IEntity>();
            entityB.BlocksGround.Returns(true);
            var defaultTileType = new TileType { BlocksGround = false };
            var board = new Board(size, defaultTileType);
            board.Add(entityA, finalPosition);
            board.Add(entityB, initialPosition);
            entityB.Position.Returns(initialPosition);
            entityA.ClearReceivedCalls();
            entityB.ClearReceivedCalls();

            // Act
            var moved = board.Move(entityB, finalPosition);

            // Assert
            moved.Should().BeFalse();
            board.GetEntities(initialPosition).Should().Equal(entityB);
            board.GetEntities(finalPosition).Should().Equal(entityA);
            entityA.DidNotReceive().OnMove(Arg.Any<Coords>());
            entityB.DidNotReceive().OnMove(Arg.Any<Coords>());
        }

        [Fact]
        public void BoardMoveFailsWithIncompatibleTile()
        {
            var size = new Size(3, 3);
            var initialPosition = new Coords(0, 1);
            var finalPosition = new Coords(2, 1);

            // Arrange
            var entity = Substitute.For<IEntity>();
            entity.BlocksGround.Returns(true);
            var floorTileType = new TileType { BlocksGround = false };
            var wallTileType = new TileType { BlocksGround = true };
            var board = new Board(size, floorTileType);
            board.Add(entity, initialPosition);
            entity.Position.Returns(initialPosition);
            board.Modify(wallTileType, finalPosition);
            entity.ClearReceivedCalls();

            // Act
            var moved = board.Move(entity, finalPosition);

            // Assert
            moved.Should().BeFalse();
            board.GetEntities(initialPosition).Should().Equal(entity);
            board.GetEntities(finalPosition).Should().BeEmpty();
            entity.DidNotReceive().OnMove(Arg.Any<Coords>());
        }

        [Fact]
        public void BoardRemovePasses()
        {
            var size = new Size(3, 3);
            var position = new Coords(1, 1);

            // Arrange
            var entity = Substitute.For<IEntity>();
            var defaultTileType = new TileType { BlocksGround = false };
            var board = new Board(size, defaultTileType);
            board.Add(entity, position);
            entity.Position.Returns(position);
            entity.ClearReceivedCalls();

            // Act
            var removed = board.Remove(entity);

            // Assert
            removed.Should().BeTrue();
            board.GetEntities(position).Should().BeEmpty();
            entity.Received().OnDestroy();
        }

        [Fact]
        public void BoardRemoveFailsWhenEntityIsNotAdded()
        {
            var size = new Size(3, 3);

            // Arrange
            var entity = Substitute.For<IEntity>();
            var defaultTileType = new TileType { BlocksGround = false };
            var board = new Board(size, defaultTileType);

            // Act
            var removed = board.Remove(entity);

            // Assert
            removed.Should().BeFalse();
            entity.DidNotReceive().OnDestroy();
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(0, 1)]
        [InlineData(2, 2)]
        public void BoardModifyPasses(int x, int y)
        {
            var size = new Size(3, 3);
            var position = new Coords(x, y);

            // Arrange
            var floorTileType = new TileType { BlocksGround = false };
            var wallTileType = new TileType { BlocksGround = true };
            var board = new Board(size, floorTileType);

            // Act
            var changed = board.Modify(wallTileType, position);

            // Assert
            changed.Should().BeTrue();
            board.GetTileType(position).Should().Be(wallTileType);
        }

        [Theory]
        [InlineData(0, -1)]
        [InlineData(-1, -1)]
        [InlineData(3, 0)]
        [InlineData(20, 30)]
        public void BoardModifyFailsWhenOutOfBounds(int x, int y)
        {
            var size = new Size(3, 3);
            var position = new Coords(x, y);

            // Arrange
            var floorTileType = new TileType { BlocksGround = false };
            var wallTileType = new TileType { BlocksGround = true };
            var board = new Board(size, floorTileType);

            // Act
            var changed = board.Modify(wallTileType, position);

            // Assert
            changed.Should().BeFalse();
            board.GetTileType(position).Should().BeNull();
        }

        [Fact]
        public void BoardModifyFailsWithIncompatibleEntity()
        {
            var size = new Size(3, 3);
            var position = new Coords(1, 1);

            // Arrange
            var entity = Substitute.For<IEntity>();
            entity.BlocksGround.Returns(true);
            var floorTileType = new TileType { BlocksGround = false };
            var wallTileType = new TileType { BlocksGround = true };
            var board = new Board(size, floorTileType);
            board.Add(entity, position);

            // Act
            var changed = board.Modify(wallTileType, position);

            // Assert
            changed.Should().BeFalse();
            board.GetTileType(position).Should().Be(floorTileType);
            board.GetEntities(position).Should().Equal(entity);
        }
    }
}