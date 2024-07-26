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
            _vacuumingSupport.PullGun.Catched += OnSlimeCathed;
            _vacuumingSupport.Movement.SetMovementSpeed(_vacuumingSupport.DetectedSpeed);
            Debug.Log("State = HUNTING");
        }

        public void Exit()
        {
            _vacuumingSupport.PullGun.Catched -= OnSlimeCathed;
        }

        public void Update()
        {
            if (_vacuumingSupport.Movement.IsEnabled() == false || _vacuumingSupport.Movement.IsMoving() == false)
            {
                _changeState?.Invoke(StatesType.Unloading);
            }

            _vacuumingSupport.Movement.Move();
        }

        private void OnSlimeCathed()
        {
            if(_vacuumingSupport.PullGun.InventoryIsFull)
            {
                _changeState?.Invoke(StatesType.Unloading);
            }
        }
    }
}