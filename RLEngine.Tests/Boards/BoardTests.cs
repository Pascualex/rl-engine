using RLEngine.Core.Boards;
using RLEngine.Core.Entities;
using RLEngine.Core.Utils;

using Xunit;
using FluentAssertions;
using System;

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
            var entityType = new EntityType { BlocksGround = true };
            var entity = new Entity(entityType);
            var defaultTileType = new TileType { BlocksGround = false };
            var board = new Board(size, defaultTileType);

            // Act
            var canAdd = board.CanAdd(entity, position);
            board.Add(entity, position);

            // Assert
            canAdd.Should().BeTrue();
            entity.IsActive.Should().BeTrue();
            entity.Position.Should().Be(position);
            board.GetEntities(position).Should().Equal(entity);
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
            var entityType = new EntityType { BlocksGround = true };
            var entity = new Entity(entityType);
            var defaultTileType = new TileType { BlocksGround = false };
            var board = new Board(size, defaultTileType);

            // Act
            var canAdd = board.CanAdd(entity, position);
            Action action = () => board.Add(entity, position);

            // Assert
            canAdd.Should().BeFalse();
            action.Should().Throw<CoordsOutOfRangeException>();
            entity.IsActive.Should().BeFalse();
            entity.Position.Should().Be(Coords.MinusOne);
            board.GetEntities(position).Should().BeEmpty();
        }

        [Fact]
        public void BoardAddPassesWithCompatibleEntity()
        {
            var size = new Size(3, 3);
            var position = new Coords(1, 1);

            // Arrange
            var entityTypeA = new EntityType
            {
                BlocksGround = true,
                IsAgent = false,
                IsGhost = false,
            };
            var entityTypeB = new EntityType
            {
                BlocksGround = true,
                IsAgent = true,
                IsGhost = true,
            };
            var entityA = new Entity(entityTypeA);
            var entityB = new Entity(entityTypeB);
            var defaultTileType = new TileType { BlocksGround = false };
            var board = new Board(size, defaultTileType);
            board.Add(entityA, position);

            // Act
            var canAdd = board.CanAdd(entityB, position);
            board.Add(entityB, position);

            // Assert
            canAdd.Should().BeTrue();
            entityA.IsActive.Should().BeTrue();
            entityA.Position.Should().Be(position);
            entityB.IsActive.Should().BeTrue();
            entityB.Position.Should().Be(position);
            board.GetEntities(position).Should().Equal(entityA, entityB);
        }

        [Fact]
        public void BoardAddFailsWithIncompatibleEntity()
        {
            var size = new Size(3, 3);
            var position = new Coords(1, 1);

            // Arrange
            var entityType = new EntityType { BlocksGround = true };
            var entityA = new Entity(entityType);
            var entityB = new Entity(entityType);
            var defaultTileType = new TileType { BlocksGround = false };
            var board = new Board(size, defaultTileType);
            board.Add(entityA, position);

            // Act
            var canAdd = board.CanAdd(entityB, position);
            Action action = () => board.Add(entityB, position);

            // Assert
            canAdd.Should().BeFalse();
            action.Should().Throw<IncompatibleTileException>();
            entityA.IsActive.Should().BeTrue();
            entityA.Position.Should().Be(position);
            entityB.IsActive.Should().BeFalse();
            entityB.Position.Should().Be(Coords.MinusOne);
            board.GetEntities(position).Should().Equal(entityA);
        }

        [Fact]
        public void BoardAddFailsWithIncompatibleTile()
        {
            var size = new Size(3, 3);
            var position = new Coords(1, 1);

            // Arrange
            var entityType = new EntityType { BlocksGround = true };
            var entity = new Entity(entityType);
            var defaultTileType = new TileType { BlocksGround = true };
            var board = new Board(size, defaultTileType);

            // Act
            var canAdd = board.CanAdd(entity, position);
            Action action = () => board.Add(entity, position);

            // Assert
            canAdd.Should().BeFalse();
            action.Should().Throw<IncompatibleTileException>();
            entity.IsActive.Should().BeFalse();
            entity.Position.Should().Be(Coords.MinusOne);
            board.GetEntities(position).Should().BeEmpty();
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
            var entityType = new EntityType { BlocksGround = true };
            var entity = new Entity(entityType);
            var defaultTileType = new TileType { BlocksGround = false };
            var board = new Board(size, defaultTileType);
            board.Add(entity, initialPosition);

            // Act
            var canMove = board.CanMove(entity, finalPosition);
            board.Move(entity, finalPosition);

            // Assert
            canMove.Should().BeTrue();
            entity.IsActive.Should().BeTrue();
            entity.Position.Should().Be(finalPosition);
            board.GetEntities(initialPosition).Should().BeEmpty();
            board.GetEntities(finalPosition).Should().Equal(entity);
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
            var entityType = new EntityType { BlocksGround = true };
            var entity = new Entity(entityType);
            var defaultTileType = new TileType { BlocksGround = false };
            var board = new Board(size, defaultTileType);
            board.Add(entity, initialPosition);

            // Act
            var canMove = board.CanMove(entity, finalPosition);
            Action action = () => board.Move(entity, finalPosition);

            // Assert
            canMove.Should().BeFalse();
            action.Should().Throw<CoordsOutOfRangeException>();
            entity.IsActive.Should().BeTrue();
            entity.Position.Should().Be(initialPosition);
            board.GetEntities(initialPosition).Should().Equal(entity);
            board.GetEntities(finalPosition).Should().BeEmpty();
        }

        [Fact]
        public void BoardMoveFailsWhenEntityIsNotAdded()
        {
            var size = new Size(3, 3);
            var finalPosition = new Coords(1, 1);

            // Arrange
            var entityType = new EntityType { BlocksGround = true };
            var entity = new Entity(entityType);
            var defaultTileType = new TileType { BlocksGround = false };
            var board = new Board(size, defaultTileType);

            // Act
            var canMove = board.CanMove(entity, finalPosition);
            Action action = () => board.Move(entity, finalPosition);

            // Assert
            canMove.Should().BeFalse();
            action.Should().Throw<EntityInactiveException>();
            entity.IsActive.Should().BeFalse();
            entity.Position.Should().Be(Coords.MinusOne);
            board.GetEntities(finalPosition).Should().BeEmpty();
        }

        [Fact]
        public void BoardMovePassesWithCompatibleEntity()
        {
            var size = new Size(3, 3);
            var initialPosition = new Coords(0, 1);
            var finalPosition = new Coords(2, 1);

            // Arrange
            var entityTypeA = new EntityType
            {
                BlocksGround = true,
                IsAgent = false,
                IsGhost = false,
            };
            var entityTypeB = new EntityType
            {
                BlocksGround = true,
                IsAgent = true,
                IsGhost = true,
            };
            var entityA = new Entity(entityTypeA);
            var entityB = new Entity(entityTypeB);
            var defaultTileType = new TileType { BlocksGround = false };
            var board = new Board(size, defaultTileType);
            board.Add(entityA, finalPosition);
            board.Add(entityB, initialPosition);

            // Act
            var canMove = board.CanMove(entityB, finalPosition);
            board.Move(entityB, finalPosition);

            // Assert
            canMove.Should().BeTrue();
            entityA.IsActive.Should().BeTrue();
            entityA.Position.Should().Be(finalPosition);
            entityB.IsActive.Should().BeTrue();
            entityB.Position.Should().Be(finalPosition);
            board.GetEntities(initialPosition).Should().BeEmpty();
            board.GetEntities(finalPosition).Should().Equal(entityA, entityB);
        }

        [Fact]
        public void BoardMoveFailsWithIncompatibleEntity()
        {
            var size = new Size(3, 3);
            var initialPosition = new Coords(0, 1);
            var finalPosition = new Coords(2, 1);

            // Arrange
            var entityType = new EntityType { BlocksGround = true };
            var entityA = new Entity(entityType);
            var entityB = new Entity(entityType);
            var defaultTileType = new TileType { BlocksGround = false };
            var board = new Board(size, defaultTileType);
            board.Add(entityA, finalPosition);
            board.Add(entityB, initialPosition);

            // Act
            var canMove = board.CanMove(entityB, finalPosition);
            Action action = () => board.Move(entityB, finalPosition);

            // Assert
            canMove.Should().BeFalse();
            action.Should().Throw<IncompatibleTileException>();
            entityA.IsActive.Should().BeTrue();
            entityA.Position.Should().Be(finalPosition);
            entityB.IsActive.Should().BeTrue();
            entityB.Position.Should().Be(initialPosition);
            board.GetEntities(initialPosition).Should().Equal(entityB);
            board.GetEntities(finalPosition).Should().Equal(entityA);
        }

        [Fact]
        public void BoardMoveFailsWithIncompatibleTile()
        {
            var size = new Size(3, 3);
            var initialPosition = new Coords(0, 1);
            var finalPosition = new Coords(2, 1);

            // Arrange
            var entityType = new EntityType { BlocksGround = true };
            var entity = new Entity(entityType);
            var floorTileType = new TileType { BlocksGround = false };
            var wallTileType = new TileType { BlocksGround = true };
            var board = new Board(size, floorTileType);
            board.Add(entity, initialPosition);
            board.Modify(wallTileType, finalPosition);

            // Act
            var canMove = board.CanMove(entity, finalPosition);
            Action action = () => board.Move(entity, finalPosition);

            // Assert
            canMove.Should().BeFalse();
            action.Should().Throw<IncompatibleTileException>();
            entity.IsActive.Should().BeTrue();
            entity.Position.Should().Be(initialPosition);
            board.GetEntities(initialPosition).Should().Equal(entity);
            board.GetEntities(finalPosition).Should().BeEmpty();
        }

        [Fact]
        public void BoardRemovePasses()
        {
            var size = new Size(3, 3);
            var position = new Coords(1, 1);

            // Arrange
            var entityType = new EntityType { BlocksGround = true };
            var entity = new Entity(entityType);
            var defaultTileType = new TileType { BlocksGround = false };
            var board = new Board(size, defaultTileType);
            board.Add(entity, position);

            // Act
            board.Remove(entity);

            // Assert
            board.GetEntities(position).Should().BeEmpty();
            entity.IsActive.Should().BeFalse();
            entity.Position.Should().Be(position);
        }

        [Fact]
        public void BoardRemoveFailsWhenEntityIsNotAdded()
        {
            var size = new Size(3, 3);

            // Arrange
            var entityType = new EntityType { BlocksGround = true };
            var entity = new Entity(entityType);
            var defaultTileType = new TileType { BlocksGround = false };
            var board = new Board(size, defaultTileType);

            // Act
            Action action = () => board.Remove(entity);

            // Assert
            action.Should().Throw<EntityInactiveException>();
            entity.IsActive.Should().BeFalse();
            entity.Position.Should().Be(Coords.MinusOne);
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
            var canModify = board.CanModify(wallTileType, position);
            board.Modify(wallTileType, position);

            // Assert
            canModify.Should().BeTrue();
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
            var canModify = board.CanModify(wallTileType, position);
            Action action = () => board.Modify(wallTileType, position);

            // Assert
            canModify.Should().BeFalse();
            action.Should().Throw<CoordsOutOfRangeException>();
            board.GetTileType(position).Should().BeNull();
        }

        [Fact]
        public void BoardModifyFailsWithIncompatibleEntity()
        {
            var size = new Size(3, 3);
            var position = new Coords(1, 1);

            // Arrange
            var entityType = new EntityType { BlocksGround = true };
            var entity = new Entity(entityType);
            var floorTileType = new TileType { BlocksGround = false };
            var wallTileType = new TileType { BlocksGround = true };
            var board = new Board(size, floorTileType);
            board.Add(entity, position);

            // Act
            var canModify = board.CanModify(wallTileType, position);
            Action action = () => board.Modify(wallTileType, position);

            // Assert
            canModify.Should().BeFalse();
            action.Should().Throw<IncompatibleTileException>();
            board.GetTileType(position).Should().Be(floorTileType);
            board.GetEntities(position).Should().Equal(entity);
        }
    }
}