using SlimeScience.Characters.Playable;
using UnityEngine;

namespace SlimeScience.FSM.States.Players
{
    public class MovementState : IBehaviour
    {
        private Player _player;

        public MovementState(Player player)
        {
            _player = player;
        }

        public void Enter()
        {
        }

        public void Exit()
        {
            _player.Movement.Disable();
        }

        public bool IsReady()
        {
            return _player.Movement.IsMoving;
        }

        public void Update()
        {
            _player.Movement.Move();
        }
    }
}