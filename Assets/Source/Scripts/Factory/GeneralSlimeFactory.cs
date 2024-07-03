using SlimeScience.Configs;
using UnityEngine;

namespace SlimeScience.Factory
{
    [CreateAssetMenu(fileName = "GeneralSlimeFactory", menuName = "Factories/Slime/General")]
    public class GeneralSlimeFactory : SlimeFactory
    {
        [SerializeField] private SlimeConfig _slimeConfig;

        protected override SlimeConfig GetConfig()
        {
            return _slimeConfig;
        }
    }
}