using SlimeScience.Upgrades;
using UnityEngine;

namespace SlimeScience.Configs.Gun
{
    [CreateAssetMenu(fileName = "AbsorbtionUpgrades", menuName = "Configs/Gun/AbsorbtionUpgrades")]
    public class GunUpgrade : ScriptableObject
    {
        private const int DefaultLevel = 0;
        private const int MaxValue = -1;

        [SerializeField] private UpgradeLevel[] _values;

        public UpgradeLevel[] Values => _values;

        public int GetCost(int level)
        {
            if (level >= _values.Length)
            {
                return MaxValue;
            }

            return _values[level].Cost;
        }

        public float GetValue(int level)
        {
            if (level >= _values.Length)
            {
                return MaxValue;
            }

            return _values[level].Value;
        }

        public int GetLevel(float value)
        {
            for (int i = 0; i < _values.Length; i++)
            {
                if (_values[i].Value == value)
                {
                    return i;
                }
            }

            return DefaultLevel;
        }
    }
}
