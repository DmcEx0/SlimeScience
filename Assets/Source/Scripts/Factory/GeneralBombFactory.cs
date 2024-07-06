using SlimeScience.Configs;
using UnityEngine;

namespace SlimeScience.Factory
{
    [CreateAssetMenu(fileName = "GeneralBomb", menuName = "Factories/General/Bomb")]
    public class GeneralBombFactory : BombFactory
    {
        [SerializeField] private BombConfig _config;

        protected override BombConfig GetConfig()
        {
            return _config;
        }
    }
}
