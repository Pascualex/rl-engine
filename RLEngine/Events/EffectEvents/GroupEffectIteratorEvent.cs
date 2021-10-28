using RLEngine.Logs;
using RLEngine.Abilities;
using RLEngine.Entities;

namespace RLEngine.Events
{
    internal class GroupEffectIteratorEvent : Event
    {
        private readonly string id;
        private readonly IEntity target;
        private readonly TargetDB targetDB;

        public GroupEffectIteratorEvent(string id, IEntity target, TargetDB targetDB)
        {
            this.id = id;
            this.target = target;
            this.targetDB = targetDB;
        }

        protected override ILog? InternalInvoke(EventContext ctx)
        {
            targetDB.Add(id, target);
            return null;
        }
    }
}
