using System.Collections.Generic;
using UnityEngine;

namespace SlimeScience.Effects
{
    public class EffectsModel
    {
        private const float DefaultModifierValue = 1.0f;

        private Dictionary<EffectModifiers, float> _effectModifiers = new Dictionary<EffectModifiers, float>()
        {
            { EffectModifiers.Force, DefaultModifierValue },
            { EffectModifiers.Radius, DefaultModifierValue },
            { EffectModifiers.Angle, DefaultModifierValue }
        };

        public float ForceModifier => _effectModifiers[EffectModifiers.Force];

        public float RadiusModifier => _effectModifiers[EffectModifiers.Radius];

        public float AngleModifier => _effectModifiers[EffectModifiers.Angle];


        public void AddModifier(EffectModifiers effect, float percent)
        {
            float value = _effectModifiers[effect];
            _effectModifiers[effect] += value * percent;
        }

        public void RemoveModifier(EffectModifiers effect, float percent)
        {
            float value = _effectModifiers[effect];
            _effectModifiers[effect] /= (DefaultModifierValue + percent);
        }

        public void ResetModifier(EffectModifiers effect)
        {
            _effectModifiers[effect] = DefaultModifierValue;
        }
    }
}
