using RLEngine.Logs;
using RLEngine.Abilities;
using RLEngine.Entities;

using System.Collections.Generic;

namespace RLEngine.Events
{
    internal class GroupEffectIteratorEvent : Event
    {
        private readonly string id;
        private readonly Entity target;
        private readonly TargetDB targetDB;

        public GroupEffectIteratorEvent(string id, Entity target, TargetDB targetDB,
        EventContext ctx) : base(ctx)
        {
            this.id = id;
            this.target = target;
            this.targetDB = targetDB;
        }

        protected override Log? InternalInvoke()
        {
            targetDB.Add(id, target);
            return null;
        }
    }
}
