using RLEngine.ViewState.Entities;

using RLEngine.Boards;
using RLEngine.Utils;

using System.Collections.Generic;
using System.Linq;

namespace RLEngine.ViewState.Boards
{
    public class BoardViewState
    {
        private readonly TileViewState[][] tiles;
        private readonly Dictionary<EntityViewState, Coords> entities = new();

        internal BoardViewState(Size size, TileType defaultTileType)
        {
            Size = size;

            tiles = new TileViewState[Size.Y][];
            for (var i = 0; i < Size.Y; i++)
            {
                tiles[i] = new TileViewState[Size.X];
                for (var j = 0; j < Size.X; j++)
                {
                    tiles[i][j] = new TileViewState(defaultTileType);
                }
            }
        }

        public Size Size { get; }

        internal bool Add(EntityViewState entity, Coords at)
        {
            if (entities.ContainsKey(entity)) return false;
            if (!Size.Contains(at)) return false;

            tiles[at.Y][at.X].Add(entity);
            entities.Add(entity, at);

            return true;
        }

        internal bool Move(EntityViewState entity, Coords to)
        {
            if (!entities.TryGetValue(entity, out var from)) return false;
            if (!Size.Contains(to)) return false;

            if (to == from) return true;

            tiles[to.Y][to.X].Add(entity);
            tiles[from.Y][from.X].Remove(entity);
            entities[entity] = to;

            return true;
        }

        internal void Remove(EntityViewState entity)
        {
            if (!entities.TryGetValue(entity, out var at)) return;

            tiles[at.Y][at.X].Remove(entity);
            entities.Remove(entity);
        }

        internal bool Modify(TileType tileType, Coords at)
        {
            if (!Size.Contains(at)) return false;

            tiles[at.Y][at.X].Modify(tileType);

            return true;
        }

        public bool TryGetCoords(EntityViewState entity, out Coords position)
        {
            return entities.TryGetValue(entity, out position);
        }

        public IEnumerable<EntityViewState> GetEntities(Coords at)
        {
            if (!Size.Contains(at)) return Enumerable.Empty<EntityViewState>();

            return tiles[at.Y][at.X].Entities;
        }

        public TileType? GetTileType(Coords at)
        {
            if (!Size.Contains(at)) return null;

            return tiles[at.Y][at.X].Type;
        }
    }
}