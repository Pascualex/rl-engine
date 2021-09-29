using RLEngine.Utils;

using YamlDotNet.Serialization;

namespace RLEngine.Serialization.Utils
{
    public abstract class Deserializable : IIdentifiable
    {
        [YamlIgnore]
        public string ID { get; set; } = DefaultID.Value;
    }
}
