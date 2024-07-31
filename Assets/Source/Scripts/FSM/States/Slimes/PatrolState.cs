using System;
using SlimeScience.Characters;
using SlimeScience.Input;
using SlimeScience.Util;
using UnityEngine;

namespace SlimeScience.FSM.States.Slimes
{
    public class PatrolState : IState
    {
        private readonly MobileObject _mobileObject;
        private readonly IDetectable _detector;

        private Action<StatesType> _changeState;

        public PatrolState(Action<StatesType> changeState, MobileObject mobileObject, IDetectable detector)
        {
            _mobileObject = mobileObject;
            _detector = detector;

            _changeState = changeState;
        }

        public void Enter()
        {
            _mobileObject.Movement.Enable();

            _mobileObject.Movement.Move();

            if (_mobileObject is VacuumingSupport support)
            {
                Debug.Log("State = PATROL");
                
                support.PullGun.Catched += OnSlimeCathed;
            }
        }

        public void Exit()
        {
            if(_mobileObject is VacuumingSupport vacuumingSupport)
            {
                vacuumingSupport.PullGun.Catched -= OnSlimeCathed;
            }
        }

        public void Update()
        {
            if (_mobileObject is Slime)
            {
                UpdateSlime();
            }
            else if (_mobileObject is VacuumingSupport)
            {
                UpdateVacuumingSupport();
            }
        }

        private void UpdateSlime()
        {
            _mobileObject.ChangeAnimationState(AnimationHashNames.Speed,
                _mobileObject.Movement.AgentSpeed);

            if (_detector.GetTargetIsNearStatus())
            {
                _changeState?.Invoke(StatesType.Fear);
                return;
            }

            if (_mobileObject.Movement.IsEnabled() == false)
            {
                _changeState?.Invoke(StatesType.SlimeIdle);
                return;
            }
            
            if(_mobileObject.Movement.IsMoving() == false)
            {
                _changeState?.Invoke(StatesType.SlimeIdle);
            }
        }

        private void UpdateVacuumingSupport()
        {
            if (_detector.GetTargetIsNearStatus())
            {
                _changeState?.Invoke(StatesType.Hunting);
                return;
            }

            _mobileObject.Movement.Move();
        }
        
        private void OnSlimeCathed()
        {
            if(_mobileObject is VacuumingSupport vacuumingSupport)
            {
                if(vacuumingSupport.PullGun.InventoryIsFull)
                {
                    _changeState?.Invoke(StatesType.Unloading);
                }
            }
        }
    }
}