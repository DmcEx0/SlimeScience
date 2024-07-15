using System;
using SlimeScience.Characters;
using SlimeScience.Util;

namespace SlimeScience.FSM.States.Players
{
    public class MovementState : IState
    {
        private readonly Player _player;
        
        private Action<StatesType> _changeState;


        public MovementState(Action<StatesType> changeState, Player player)
        {
            _player = player;
            _changeState = changeState;
        }

        public void Enter()
        {
        }

        public void Exit()
        {
            // _player.Movement.Disable();
        }

        public void Update()
        {
            _player.ChangeAnimationState(AnimationHashNames.Speed, _player.Movement.AgentSpeed);

            if (_player.Movement.IsMoving() == false)
            {
                _changeState.Invoke(StatesType.PlayerIdle);
            }
            
            _player.Movement.Move();
        }
    }
}