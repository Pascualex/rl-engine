using RLEngine.ViewState.Entities;

using RLEngine.Boards;

using System.Collections.Generic;

namespace RLEngine.ViewState.Boards
{
    internal class TileViewState
    {
        private readonly HashSet<EntityViewState> entities = new();

        public TileViewState(TileType type)
        {
            Type = type;
        }

        public IEnumerable<EntityViewState> Entities => entities;
        public TileType Type { get; private set; }

        public void Add(EntityViewState entity)
        {
            entities.Add(entity);
        }

        public void Remove(EntityViewState entity)
        {
            entities.Remove(entity);
        }

        public void Modify(TileType type)
        {
            Type = type;
        }
    }
}