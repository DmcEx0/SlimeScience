using System;
using SlimeScience.Characters;
using SlimeScience.Input;
using SlimeScience.Util;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SlimeScience.FSM.States
{
    public class SlimeIdleState : IState
    {
        private readonly Slime _slime;
        
        private IDetectable _detector;

        private readonly float _minDelay = 1f;
        private readonly float _maxDelay = 3f;

        private float _accelerationTime;
        private float _currentDelay;

        private Action<StatesType> _changeState;

        public SlimeIdleState(Action<StatesType> changeState, Slime slime, IDetectable detector)
        {
            _changeState = changeState;
            _slime = slime;
            _detector = detector;
        }

        public void Enter()
        {
            _slime.Movement.Disable();

            DefineRandomDelay();
        }

        public void Exit()
        {
        }

        public void Update()
        {
            _slime.ChangeAnimationState(AnimationHashNames.Speed, 0f);
            
            if(_detector.GetTargetIsNearStatus())
            {
                _changeState?.Invoke(StatesType.Fear);
            }
            
            _accelerationTime += Time.deltaTime;
            
            if (_accelerationTime >= _currentDelay)
            {
                _changeState?.Invoke(StatesType.Patrol);
                _accelerationTime = 0;
            }
        }

        private void DefineRandomDelay()
        {
            _currentDelay = Random.Range(_minDelay, _maxDelay);
        }
    }
}