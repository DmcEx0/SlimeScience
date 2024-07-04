using SlimeScience.Configs;
using UnityEngine;

namespace SlimeScience.Factory
{
    [CreateAssetMenu(fileName = "GeneralPlayerFactory", menuName = "Factories/General/Player")]
    public class GeneralPlayerFactory : PlayerFactory
    {
        [SerializeField] private PlayerConfig _playerConfig;

        protected override PlayerConfig GetConfig()
        {
            return _playerConfig;
        }
    }
}