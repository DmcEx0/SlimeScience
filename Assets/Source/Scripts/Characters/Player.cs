using System.Collections.Generic;
using SlimeScience.Configs;
using SlimeScience.Equipment.Guns;
using SlimeScience.Saves;
using SlimeScience.Util;
using UnityEngine;

namespace SlimeScience.Characters
{
    public class Player : MobileObject, ISeekable
    {
        [SerializeField] private Collider _collider;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private PullGun _pullGun;

        private PlayerConfig _config;
        private GameVariables _gameVariables;

        private float _rangeVacuum;

        private float _defaultAcceleration;
        private float _currentAcceleration;

        public PullGun PullGun => _pullGun;

        private void Update()
        {
            UpdateStateMachine();
        }

        public void InitGun(GameVariables gameVariables)
        {
            _gameVariables = gameVariables;

            _pullGun.Init(gameVariables);
        }

        public List<Slime> ReleaseSlimes()
        {
            List<Slime> releaseSlimes = new List<Slime>(_pullGun.ReleaseInventory());
            _pullGun.RenderInventory();

            return releaseSlimes;
        }

        public void TranslateToShip(Transform shipPlaceForPlayer, ShipConfig shipConfig)
        {
            int agentTypeId = NavMeshAgentUtil.GetAgentTypeID(AgentTypeIds.Ship);
            ChangeAgent(agentTypeId);
            SetMovementStats(shipConfig);
            SetMovementSpeed(_gameVariables.ShipSpeed);
            _collider.enabled = false;

            transform.position = shipPlaceForPlayer.position;
            transform.rotation = shipPlaceForPlayer.rotation;
        }

        public void LeaveShip()
        {
            int agentTypeId = NavMeshAgentUtil.GetAgentTypeID(AgentTypeIds.Player);
            ChangeAgent(agentTypeId);
            SetMovementStats(_config);
            _collider.enabled = true;
        }

        public void SetMovementModifiers(float accelerationMultiplier)
        {
            _currentAcceleration = _defaultAcceleration * accelerationMultiplier;
            Movement.SetAcceleration(_currentAcceleration);
        }

        public void ResetMovementModifiers()
        {
            _currentAcceleration = _defaultAcceleration;
            Movement.SetAcceleration(_defaultAcceleration);
        }

        protected override void Init(MobileObjectConfig config)
        {
            if (config is not PlayerConfig playerConfig)
            {
                return;
            }

            _config = playerConfig;

            _rangeVacuum = _config.RangeVacuum;

            _defaultAcceleration = config.Acceleration;
            _currentAcceleration = _defaultAcceleration;

            SetRigidbodySetting(_rigidbody);
        }

        protected override void SetRigidbodySetting(Rigidbody rigidbody)
        {
            base.SetRigidbodySetting(rigidbody);

            rigidbody.isKinematic = true;
        }
    }
}