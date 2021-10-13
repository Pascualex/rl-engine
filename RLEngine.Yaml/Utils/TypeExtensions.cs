using System.Collections.Generic;
using System;
using System.Linq;
using System.Reflection;

namespace RLEngine.Yaml.Utils
{
    public static class TypeExtensions
    {
        private const BindingFlags flags = BindingFlags.Instance
                                         | BindingFlags.Public
                                         | BindingFlags.FlattenHierarchy;

        public static IEnumerable<PropertyInfo> GetPublicProperties(this Type type)
        {
            if (!type.IsInterface) return type.GetProperties(flags);

            return (new Type[] { type })
                .Concat(type.GetInterfaces())
                .Reverse()
                .SelectMany(i => i.GetProperties());
        }

        public static PropertyInfo? GetPublicProperty(this Type type, string name)
        {
            if (!type.IsInterface) return type.GetProperty(name, flags);

            return (new Type[] { type })
                .Concat(type.GetInterfaces())
                .Single(i => i.GetProperty(name) != null)
                .GetProperty(name);
        }
    }
}
