using SlimeScience.Configs;
using SlimeScience.Equipment.Guns;
using SlimeScience.Saves;
using UnityEngine;

namespace SlimeScience.Characters.Playable
{
    public class Player : MobileObject
    {
        [SerializeField] private PullGun _pullGun;

        private float _rangeVacuum;

        private void Update()
        {
            UpdateStateMachine();
        }

        public void InitGun(GameVariables gameVariables)
        {
            _pullGun.Init(gameVariables);
        }

        public void ReleaseSlimes(Vector3 position)
        {
            _pullGun.ReleaseInventory(position);
        }

        protected override void Init(MobileObjectConfig config)
        {
            if (config is not PlayerConfig)
                return;

            var playerConfig = config as PlayerConfig;

            _rangeVacuum = playerConfig.RangeVacuum;
        }
    }
}