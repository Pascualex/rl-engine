using RLEngine.Utils;

using System;
using System.Collections.Generic;

namespace RLEngine.Serialization.Yaml
{
    public class SerializationQueue<T> where T : IIdentifiable
    {
        private readonly Queue<(T, Type)> queue = new();
        private readonly Dictionary<Type, Dictionary<string, T>> discovered = new();

        public int Count => queue.Count;

        public void Enqueue(T identifiable, Type type)
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

        public (T, Type) Dequeue()
        {
            return queue.Dequeue();
        }

        public bool TryGetValue(string id, Type type, out T value)
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