using UnityEngine;
using SlimeScience.Characters.Playable;

namespace SlimeScience.FSM.States.Players
{
    public class IdleState : IBehaviour
    {
        private Player _player;

        public IdleState(Player player)
        {
            _player = player;
        }

        public void Enter()
        {
            _player.Movement.Enable();
        }

        public void Exit()
        {
        }

        public bool IsReady()
        {
            return _player.Movement.IsMoving == false;
        }

        public void Update()
        {
            _player.Movement.Move();
        }
    }
}