using SlimeScience.Effects;
using UnityEngine;

namespace SlimeScience.Traps
{
    [RequireComponent(typeof(Collider))]
    public class Trap : MonoBehaviour
    {
        [field: SerializeField] public EffectModifiers Modifier { get; private set; }

        [field: SerializeField, Range(-1, 0)] public float ModifierPercent { get; private set; }

        [field: SerializeField] public float DurationInSeconds { get; private set; }
    }
}
