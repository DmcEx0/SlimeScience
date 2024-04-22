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
            Debug.Log("Idle");
        }

        public void Exit()
        {
        }

        public bool IsReady()
        {
            return _player.Movement.IsMoving == false && _player.Movement.IsGettingNewDirection() == false;
        }

        public void Update()
        {
        }
    }
}