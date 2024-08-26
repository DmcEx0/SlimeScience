using System;
using SlimeScience.Characters;
using SlimeScience.Util;

namespace SlimeScience.FSM.States.Players
{
    public class PlayerIdleState : IState
    {
        private Player _player;

        private Action<StatesType> _changeState;

        public PlayerIdleState(Action<StatesType> changeState, Player player)
        {
            _player = player;
            _changeState = changeState;
        }

        public void Enter()
        {
        }

        public void Exit()
        {
        }

        public void Update()
        {
            _player.ChangeAnimationState(AnimationHashNames.Speed, 0f);

            _player.Movement.Move();

            if (_player.Movement.IsMoving())
            {
                _changeState?.Invoke(StatesType.Movement);
            }
        }
    }
}