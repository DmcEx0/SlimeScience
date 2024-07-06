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

            if (_mobileObject is VacuumingSupport)
            {
                Debug.Log("State = PATROL");
            }
        }

        public void Exit()
        {
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
            }

            if (_mobileObject.Movement.IsMoving() == false)
            {
                _changeState?.Invoke(StatesType.SlimeIdle);
            }
        }

        private void UpdateVacuumingSupport()
        {
            if (_detector.GetTargetIsNearStatus())
            {
                _changeState?.Invoke(StatesType.Hunting);
            }

            if (_mobileObject.Movement.IsMoving() == false)
            {
                _changeState?.Invoke(StatesType.Unloading);
            }

            _mobileObject.Movement.Move();
        }
    }
}