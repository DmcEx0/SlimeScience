using SlimeScience.Characters.Slimes;
using SlimeScience.Configs;
using SlimeScience.Equipment.Guns;
using SlimeScience.Saves;
using System.Collections.Generic;
using UnityEngine;

namespace SlimeScience.Characters.Playable
{
    public class Player : MobileObject
    {
        [SerializeField] private Rigidbody _rigidbody;
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

        public List<Slime> ReleaseSlimes(Vector3 position)
        {
            return _pullGun.ReleaseInventory(position);
        }

        protected override void Init(MobileObjectConfig config)
        {
            if (config is not PlayerConfig)
                return;

            var playerConfig = config as PlayerConfig;

            _rangeVacuum = playerConfig.RangeVacuum;

            SetRigidbodySetting(_rigidbody);
        }

        protected override void SetRigidbodySetting(Rigidbody rigidbody)
        {
            base.SetRigidbodySetting(rigidbody);

            rigidbody.isKinematic = true;
        }
    }
}