using RLEngine.Entities;
using RLEngine.Utils;

using System.Collections.Generic;
using System.Linq;

namespace RLEngine.Boards
{
    public class Board
    {
        private readonly Tile[][] tiles;
        private readonly Dictionary<Entity, Coords> entities = new();

        public Board(Size size, TileType defaultTileType)
        {
            Size = size;
            tiles = new Tile[Size.Y][];

            for (var i = 0; i < Size.Y; i++)
            {
                tiles[i] = new Tile[Size.X];
                for (var j = 0; j < Size.X; j++)
                {
                    tiles[i][j] = new Tile(defaultTileType);
                }
            }
        }

        public Size Size { get; }

        public bool Add(Entity entity, Coords at)
        {
            if (entities.ContainsKey(entity)) return false;
            if (!Size.Contains(at)) return false;

            if (!tiles[at.Y][at.X].Add(entity)) return false;
            entities.Add(entity, at);

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

            return true;
        }

        public bool Remove(Entity entity)
        {
            if (!entities.TryGetValue(entity, out var at)) return false;

            tiles[at.Y][at.X].Remove(entity);
            entities.Remove(entity);

            return true;
        }

        public bool Modify(TileType tileType, Coords at)
        {
            if (!Size.Contains(at)) return false;

            return tiles[at.Y][at.X].Modify(tileType);
        }

        public bool CanAdd(Entity entity, Coords at)
        {
            if (entities.ContainsKey(entity)) return false;
            if (!Size.Contains(at)) return false;
            return tiles[at.Y][at.X].CanAdd(entity);
        }

        public bool CanMove(Entity entity, Coords to)
        {
            if (!entities.TryGetValue(entity, out var from)) return false;
            if (!Size.Contains(to)) return false;
            if (to == from) return true;
            return tiles[to.Y][to.X].CanAdd(entity);
        }

        public bool CanRemove(Entity entity)
        {
            return entities.ContainsKey(entity);
        }

        public bool CanModify(TileType tileType, Coords at)
        {
            if (!Size.Contains(at)) return false;
            return tiles[at.Y][at.X].CanModify(tileType);
        }

        public bool TryGetCoords(Entity entity, out Coords position)
        {
            return entities.TryGetValue(entity, out position);
        }

        public IEnumerable<Entity> GetEntities(Coords at)
        {
            if (!Size.Contains(at)) return Enumerable.Empty<Entity>();

            return tiles[at.Y][at.X].Entities;
        }

        public TileType? GetTileType(Coords at)
        {
            if (!Size.Contains(at)) return null;

            return tiles[at.Y][at.X].Type;
        }
    }
}