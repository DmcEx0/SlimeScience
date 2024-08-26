using System;
using SlimeScience.Input;
using UnityEngine;

namespace SlimeScience.FSM.States
{
    public class HuntingState : IState
    {
        private readonly VacuumingSupport _vacuumingSupport;
        private readonly IDetectable _detector;

        private readonly Action<StatesType> _changeState;

        public HuntingState(Action<StatesType> changeState, VacuumingSupport vacuumingSupport, IDetectable detector)
        {
            _changeState = changeState;
            _vacuumingSupport = vacuumingSupport;
            _detector = detector;
        }

        public void Enter()
        {
            if (_vacuumingSupport.PullGun.InventoryIsFull)
            {
                _changeState?.Invoke(StatesType.Unloading);
            }

            _vacuumingSupport.PullGun.Catched += OnSlimeCathed;
            _vacuumingSupport.Movement.SetMovementSpeed(_vacuumingSupport.DetectedSpeed);
        }

        public void Exit()
        {
            _vacuumingSupport.PullGun.Catched -= OnSlimeCathed;
        }

        public void Update()
        {
            _vacuumingSupport.Movement.Move(false);

            if (_detector.HasTargetTransforms() == false)
            {
                _changeState.Invoke(StatesType.Patrol);
            }
        }

        private void OnSlimeCathed()
        {
            if (_vacuumingSupport.PullGun.InventoryIsFull)
            {
                _changeState?.Invoke(StatesType.Unloading);

                return;
            }

            _vacuumingSupport.Movement.Disable();

            _changeState?.Invoke(StatesType.Patrol);
        }
    }
}