using RLEngine.ViewState.Boards;
using RLEngine.ViewState.Entities;

using RLEngine.Games;
using RLEngine.Logs;
using RLEngine.Entities;

using System.Collections.Generic;

namespace RLEngine.ViewState.Games
{
    public class GameViewState
    {
        private readonly Dictionary<Entity, EntityViewState> entities = new();

        public GameViewState(GameContent content)
        {
            Board = new(content.BoardSize, content.FloorType);
        }

        public BoardViewState Board { get; }

        public bool IsSupported(Log log)
        {
            if (log is       DamageLog) return true;
            if (log is  DestructionLog) return true;
            if (log is      HealingLog) return true;
            if (log is ModificationLog) return true;
            if (log is     MovementLog) return true;
            if (log is   ProjectileLog) return true;
            if (log is        SpawnLog) return true;
            return false;
        }

        public bool Update(Log log)
        {
            if (log is       DamageLog       damageLog) return Update(      damageLog);
            if (log is  DestructionLog  destructionLog) return Update( destructionLog);
            if (log is      HealingLog      healingLog) return Update(     healingLog);
            if (log is ModificationLog modificationLog) return Update(modificationLog);
            if (log is     MovementLog     movementLog) return Update(    movementLog);
            if (log is   ProjectileLog   projectileLog) return Update(  projectileLog);
            if (log is        SpawnLog        spawnLog) return Update(       spawnLog);
            return true;
        }

        private bool Update(DamageLog log)
        {
            return true;
        }

        private bool Update(DestructionLog log)
        {
            if (!entities.TryGetValue(log.Entity, out var entity)) return false;

            Board.Remove(entity);

            return true;
        }

        private bool Update(HealingLog log)
        {
            return true;
        }

        private bool Update(ModificationLog log)
        {
            Board.Modify(log.NewType, log.At);

            return true;
        }

        private bool Update(MovementLog log)
        {
            if (!entities.TryGetValue(log.Entity, out var entity)) return false;

            Board.Move(entity, log.To);

            return true;
        }

        private bool Update(ProjectileLog log)
        {
            return true;
        }

        private bool Update(SpawnLog log)
        {
            if (entities.ContainsKey(log.Entity)) return false;

            var entity = new EntityViewState(log.Entity);
            if (!Board.Add(entity, log.At)) return false;
            entities.Add(log.Entity, entity);

            return true;
        }
    }
}