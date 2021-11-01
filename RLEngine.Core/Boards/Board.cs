using RLEngine.Core.Entities;
using RLEngine.Core.Utils;

using System;
using System.Collections.Generic;

namespace RLEngine.Core.Boards
{
    internal class Board : IBoard
    {
        private readonly Tile[][] tiles;

        public Board(Size size, TileType defaultTileType)
        {
            Size = size;

            tiles = new Tile[Size.Y][];
            for (var i = 0; i < Size.Y; i++)
            {
                tiles[i] = new Tile[Size.X];
                for (var j = 0; j < Size.X; j++)
                {
                    tiles[i][j] = new Tile(new Coords(j, i), defaultTileType);
                }
            }
        }

        public Size Size { get; }

        public void Add(IEntity entity, Coords at)
        {
            if (entity.IsActive) throw new EntityActiveException(entity);
            if (!Size.Contains(at)) throw new CoordsOutOfRangeException(at, Size);

            tiles[at.Y][at.X].Add(entity);
            entity.OnSpawn(at);
        }

        public void Move(IEntity entity, Coords to)
        {
            if (!entity.IsActive) throw new EntityInactiveException(entity);
            if (!Size.Contains(to)) throw new CoordsOutOfRangeException(to, Size);

            var from = entity.Position;
            if (to == from) return;

            tiles[to.Y][to.X].Add(entity);
            tiles[from.Y][from.X].Remove(entity);
            entity.OnMove(to);
        }

        public void Remove(IEntity entity)
        {
            if (!entity.IsActive) throw new EntityInactiveException(entity);

            var at = entity.Position;

            tiles[at.Y][at.X].Remove(entity);
            entity.OnDestroy();
        }

        public void Modify(TileType tileType, Coords at)
        {
            if (!Size.Contains(at)) throw new CoordsOutOfRangeException(at, Size);

            tiles[at.Y][at.X].Modify(tileType);
        }

        public bool CanAdd(IEntity entity, Coords at)
        {
            if (entity.IsActive) return false;
            if (!Size.Contains(at)) return false;
            return tiles[at.Y][at.X].CanAdd(entity);
        }

        public bool CanMove(IEntity entity, Coords to)
        {
            if (!entity.IsActive) return false;
            if (!Size.Contains(to)) return false;
            var from = entity.Position;
            if (to == from) return true;
            return tiles[to.Y][to.X].CanAdd(entity);
        }

        public bool CanModify(TileType tileType, Coords at)
        {
            if (!Size.Contains(at)) return false;
            return tiles[at.Y][at.X].CanModify(tileType);
        }

        public IReadOnlyCollection<IEntity> GetEntities(Coords at)
        {
            if (!Size.Contains(at)) return Array.Empty<IEntity>();

            return tiles[at.Y][at.X].Entities;
        }

        public TileType? GetTileType(Coords at)
        {
            if (!Size.Contains(at)) return null;

            return tiles[at.Y][at.X].Type;
        }
    }
}