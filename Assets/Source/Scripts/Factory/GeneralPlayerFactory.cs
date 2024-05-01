using SlimeScience.Configs;
using UnityEngine;

namespace SlimeScience.Factory
{
    [CreateAssetMenu(fileName = "GeneralPlayerFactory", menuName = "Factories/Player/General")]
    public class GeneralPlayerFactory : PlayerFactory
    {
        [SerializeField] private PlayerConfig _playerConfig;

        protected override PlayerConfig GetConfig()
        {
            return _playerConfig;
        }
    }
}