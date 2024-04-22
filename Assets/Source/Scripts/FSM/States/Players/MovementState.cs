using SlimeScience.Characters.Playable;
using UnityEngine;

namespace SlimeScience.FSM.States.Players
{
    public class MovementState : IBehaviour
    {
        private Player _player;

        public MovementState(Player _player)
        {
            this._player = _player;
        }

        public void Enter()
        {
            Debug.Log("Moving");
        }

        public void Exit()
        {
            _player.Movement.Disable();
        }

        public bool IsReady()
        {
            return _player.Movement.IsGettingNewDirection();
        }

        public void Update()
        {
            _player.Movement.IsGettingNewDirection();
            _player.Movement.Move();
        }
    }
}