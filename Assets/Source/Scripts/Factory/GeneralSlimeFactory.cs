using System;
using SlimeScience.Characters;
using SlimeScience.Configs;
using SlimeScience.Configs.Slimes;
using UnityEngine;
using UnityEngine.Serialization;

namespace SlimeScience.Factory
{
    [CreateAssetMenu(fileName = "GeneralSlimeFactory", menuName = "Factories/General/Slime")]
    public class GeneralSlimeFactory : SlimeFactory
    {
        [SerializeField] private SlimeConfig _generalSlimeConfig;
        [SerializeField] private SlimeTypesConfig _slimeTypesConfig;

        protected override SlimeConfig GetConfig()
        {
            return _generalSlimeConfig;
        }

        protected override SlimeTypeValues GetTypeConfig(SlimeType type)
        {
            foreach (var slimeType in _slimeTypesConfig.SlimeTypeValues)
            {
                if (slimeType.Type == type)
                {
                    return slimeType;
                }
            }

            return _slimeTypesConfig.SlimeTypeValues[0];
        }
    }
}