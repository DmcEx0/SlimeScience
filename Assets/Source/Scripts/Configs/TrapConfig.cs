using System;
using SlimeScience.Effects;
using SlimeScience.Traps;
using UnityEngine;

namespace SlimeScience.Configs
{
    [Serializable]
    public class TrapConfig
    {
        [field: SerializeField] public Trap Prefab;

        [field: SerializeField] public EffectModifiers Modifier { get; private set; }

        [field: SerializeField]
        [field: Range(-1, 0)] public float ModifierPercent { get; private set; }

        [field: SerializeField] public float DurationInSeconds { get; private set; }
    }
}