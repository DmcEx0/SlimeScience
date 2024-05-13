using UnityEngine;

namespace SlimeScience.Configs.Gun
{
    [CreateAssetMenu(fileName = "RadiusUpgrades", menuName = "Configs/Gun/RadiusUpgrades")]
    public class RadiusUpgrades : ScriptableObject
    {
        [SerializeField] private int[] _values;

        public int[] Values => _values;
    }
}
