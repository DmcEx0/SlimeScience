using UnityEngine;

namespace SlimeScience.Configs.Gun
{
    [CreateAssetMenu(fileName = "CapacityUpgrades", menuName = "Configs/Gun/CapacityUpgrades")]
    public class CapacityUpgrades : ScriptableObject
    {
        [SerializeField] private int[] _values;

        public int[] Values => _values;
    }
}
