using UnityEngine;

namespace SlimeScience.Configs.Gun
{
    [CreateAssetMenu(fileName = "AbsorbtionUpgrades", menuName = "Configs/Gun/AbsorbtionUpgrades")]
    public class AbsorbtionUpgrades : ScriptableObject
    {
        [SerializeField] private int[] _values;

        public int[] Values => _values;
    }
}
