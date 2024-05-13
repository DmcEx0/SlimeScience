using UnityEngine;

namespace SlimeScience.Configs.Gun
{
    [CreateAssetMenu(fileName = "DistanceUpgrades", menuName = "Configs/Gun/DistanceUpgrades")]
    public class DistanceUpgrades : ScriptableObject
    {
        [SerializeField] private int[] _values;

        public int[] Values => _values;
    }
}
