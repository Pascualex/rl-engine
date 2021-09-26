using System.ComponentModel;

namespace RLEngine.Content.Utils
{
    public static class ReflectionUtils
    {
        public static void Assign<T>(T obj, string propertyName, string value)
        {
            if (obj is null) return;

            var property = typeof(T).GetProperty(propertyName);
            var typeDescriptor = TypeDescriptor.GetConverter(property.PropertyType);
            property.SetValue(obj, typeDescriptor.ConvertFromInvariantString(value));
        }
    }
}
