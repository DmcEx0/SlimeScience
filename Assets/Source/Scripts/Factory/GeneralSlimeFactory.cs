using SlimeScience.Configs;
using UnityEngine;

namespace SlimeScience.Factory
{
    [CreateAssetMenu(fileName = "GeneralSlimeFactory", menuName = "Factories/General/Slime")]
    public class GeneralSlimeFactory : SlimeFactory
    {
        [SerializeField] private SlimeConfig _slimeConfig;

        protected override SlimeConfig GetConfig()
        {
            return _slimeConfig;
        }
    }
}