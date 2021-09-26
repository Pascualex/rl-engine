﻿using RLEngine.Abilities;
using RLEngine.Actions;

using System.Collections.Generic;

using YamlDotNet.Serialization;

namespace RLEngine.Serialization.Abilities
{
    public class Effect : IEffect
    {
        public EffectType Type { get; set; } = EffectType.Combined;
        [YamlIgnore]
        public IEnumerable<IEffect> Effects => SerializedEffects;
        public bool IsParallel { get; set; } = false;
        public ActionAmount Amount { get; set; } = new();

        [YamlMember(Alias = "Effects")]
        public List<Effect> SerializedEffects { get; set; } = new();
    }
}