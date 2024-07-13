using System.Collections.Generic;
using System.Data;
using SlimeScience.Configs;
using SlimeScience.Equipment.Guns;
using SlimeScience.Saves;
using UnityEngine;

namespace SlimeScience.Characters
{
    public class Player : MobileObject, ISeekable
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private PullGun _pullGun;

        private PlayerConfig _config;

        private float _rangeVacuum;

        private void Update()
        {
            UpdateStateMachine();
        }

        public void InitGun(GameVariables gameVariables)
        {
            _pullGun.Init(gameVariables);
        }

        public List<Slime> ReleaseSlimes()
        {
            List<Slime> releaseSlimes =  new List<Slime>(_pullGun.ReleaseInventory());
            _pullGun.RenderInventory();

            return releaseSlimes;
        }

        public void TranslateToShip(Transform shipPlaceForPlayer, float moveSpeed, float rotateSpeed)
        {
            Movement.SetMovementSpeed(moveSpeed);
            Movement.SetRotateSpeed(rotateSpeed);
            
            transform.position = shipPlaceForPlayer.position;
            transform.rotation = shipPlaceForPlayer.rotation;
        }

        public void LeaveShip()
        {
            Movement.SetMovementSpeed(_config.BaseSpeed);
            Movement.SetRotateSpeed(_config.AngularSpeed);
        }

        protected override void Init(MobileObjectConfig config)
        {
            if (config is not PlayerConfig playerConfig)
                return;

            _config = playerConfig;

            _rangeVacuum = _config.RangeVacuum;

            SetRigidbodySetting(_rigidbody);
        }

        protected override void SetRigidbodySetting(Rigidbody rigidbody)
        {
            base.SetRigidbodySetting(rigidbody);

            rigidbody.isKinematic = true;
        }
    }
}