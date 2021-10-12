using RLEngine.Utils;

using System;
using System.Collections.Generic;

namespace RLEngine.Yaml.Serialization
{
    public class SerializationQueue
    {
        private readonly Queue<(IIdentifiable, Type)> queue = new();
        private readonly Dictionary<Type, Dictionary<string, IIdentifiable>> discovered = new();

        public int Count => queue.Count;

        public void Enqueue(IIdentifiable identifiable, Type type)
        {
            if (!discovered.TryGetValue(type, out var discoveredForType))
            {
                discoveredForType = new();
                discovered.Add(type, discoveredForType);
            }

            if (discoveredForType.TryGetValue(identifiable.ID, out var present))
            {
                if (!Equals(identifiable, present))
                {
                    var message = $"For type {type} the ID \"{identifiable.ID} already exists\"";
                    throw new InvalidOperationException(message);
                }
            }
            else
            {
                discoveredForType.Add(identifiable.ID, identifiable);
                queue.Enqueue((identifiable, type));
            }
        }

        public (IIdentifiable, Type) Dequeue()
        {
            return queue.Dequeue();
        }

        public bool TryGetValue(string id, Type type, out IIdentifiable value)
        {
            if (discovered.TryGetValue(type, out var discoveredForType))
            {
                return discoveredForType.TryGetValue(id, out value);
            }
            value = default!;
            return false;
        }
    }
}