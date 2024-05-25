using System;
using SlimeScience.Characters.Slimes;
using SlimeScience.Input;
using SlimeScience.Util;
using UnityEngine;

namespace SlimeScience.FSM.States.Slimes
{
    public class PatrolState : IState
    {
        private readonly Slime _slime;
        private readonly IDetectable _detector;

        private Action<StatesType> _changeState;

        public PatrolState(Action<StatesType> changeState, Slime slime, IDetectable detector)
        {
            _slime = slime;
            _detector = detector;

            _changeState = changeState;
        }

        public void Enter()
        { 
            _slime.Movement.Enable();
            _slime.Movement.Move();
        }

        public void Exit()
        {
            _slime.Movement.Disable();
        }

        public void Update()
        {
            _slime.ChangeAnimationState(AnimationHashNames.Speed, _slime.Movement.AgentSpeed); // хз, почему, но работает ток в апдейте
            
            if(_detector.GetPlayerIsNearStatus())
            {
                _changeState?.Invoke(StatesType.Fear);
            }
            
            if (_slime.Movement.IsPlaceable && _slime.Movement.IsMoving == false)
            {
               _changeState?.Invoke(StatesType.SlimeIdle);
            }
        }
    }
}