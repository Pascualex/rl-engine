using System;
using System.Collections.Generic;

using RLEngine.Entities;
using RLEngine.Utils;

namespace  RLEngine.Boards
{
    public class Board : IReadOnlyBoard
    {
        public Size Size { get; }

        private readonly Tile[][] tiles;
        private readonly Dictionary<Entity, Coords> entities = new();
        private readonly IBoardView view;

        public Board(Size size, ITileType defaultTileType, IBoardView view)
        {
            Size = size;
            tiles = new Tile[Size.Y][];
            this.view = view;

            for (var i = 0; i < Size.Y; i++)
            {
                tiles[i] = new Tile[Size.X];
                for (var j = 0; j < Size.X; j++)
                {
                    tiles[i][j] = new Tile(defaultTileType);
                    view.Modify(new Coords(j, i), defaultTileType);
                }
            }
        }

        public bool Add(Entity entity, Coords at)
        {
            if (entities.ContainsKey(entity)) return false;
            if (!Size.Contains(at)) return false;

            if (!tiles[at.Y][at.X].Add(entity)) return false;
            entities.Add(entity, at);
            view.Add(entity, at);

            return true;
        }

        public bool Move(Entity entity, Coords to)
        {
            if (!entities.TryGetValue(entity, out var from)) return false;
            if (!Size.Contains(to)) return false;

            if (to == from) return true;

            if (!tiles[to.Y][to.X].Add(entity)) return false;
            tiles[from.Y][from.X].Remove(entity);
            entities[entity] = to;
            view.Move(entity, to);

            return true;
        }

        public bool Remove(Entity entity)
        {
            if (!entities.TryGetValue(entity, out var at)) return false;

            tiles[at.Y][at.X].Remove(entity);
            entities.Remove(entity);
            view.Remove(entity);

            return true;
        }

        public bool ChangeTileType(ITileType tileType, Coords at)
        {
            if (!Size.Contains(at)) return false;

            if (!tiles[at.Y][at.X].ChangeType(tileType)) return false;
            view.Modify(at, tileType);

            return true;
        }

        public IReadOnlyTile? GetTile(Coords at)
        {
            if (!Size.Contains(at)) return null;

            return tiles[at.Y][at.X];
        }

        public IEnumerable<IReadOnlyTile> GetTiles()
        {
            return GetTiles(Coords.Zero, new Coords(Size.X - 1, Size.Y - 1));
        }

        public IEnumerable<IReadOnlyTile> GetTiles(Coords from, Coords to)
        {
            var lower = new Coords(Math.Min(from.X, to.X), Math.Min(from.Y, to.Y));
            var higher = new Coords(Math.Max(from.X, to.X), Math.Max(from.Y, to.Y));
            for (var i = lower.Y; i <= higher.Y; i++)
            {
                for (var j = lower.X; j <= higher.Y; j++)
                {
                    var tile = GetTile(new Coords(j, i));
                    if (tile is not null) yield return tile;
                }
            }
        }
    }
}