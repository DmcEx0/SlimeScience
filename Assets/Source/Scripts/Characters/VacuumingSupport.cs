using System.Collections.Generic;
using SlimeScience.Characters;
using SlimeScience.Configs;
using SlimeScience.Equipment.Guns;
using SlimeScience.Saves;
using UnityEngine;

namespace SlimeScience
{
    public class VacuumingSupport : MobileObject, ISeekable
    {
        [SerializeField] private PullGun _pullGun;
        [SerializeField] private Rigidbody _rigidbody;
        
        private Coroutine _resetVelocityCoroutine;
        private int _inventoryCapacity;
        private Vector3 _unloadPosition;

        public Vector3 UnloadPosition => _unloadPosition;
        public PullGun PullGun => _pullGun;
        
        public float DetectedSpeed { get; private set; }
        public float BaseSpeed{ get; private set; }

        private void OnDisable()
        {
            if (_resetVelocityCoroutine != null)
            {
                StopCoroutine(_resetVelocityCoroutine);
            }
        }

        private void Update()
        {
            UpdateStateMachine();
        }
        
        public List<Slime> ReleaseSlimes()
        {
            return _pullGun.ReleaseInventory();
        }

        public void SetUnloadPosition(Vector3 position)
        {
            _unloadPosition = position;
        }

        public void InitGun(GameVariables gameVariables)
        {
            _pullGun.Init(gameVariables, _inventoryCapacity);
        }

        protected override void Init(MobileObjectConfig config)
        {
            if (config is not VacuumingSupportConfig)
                return;

            SetRigidbodySetting(_rigidbody);

            VacuumingSupportConfig vacuumingSupportConfig = config as VacuumingSupportConfig;

            BaseSpeed = vacuumingSupportConfig.BaseSpeed;
            DetectedSpeed = vacuumingSupportConfig.DetectedSpeed;
            _inventoryCapacity = vacuumingSupportConfig.InventoryCapacity;
        }

        protected override void SetRigidbodySetting(Rigidbody rigidbody)
        {
            base.SetRigidbodySetting(rigidbody);

            rigidbody.isKinematic = true;
        }
    }
}