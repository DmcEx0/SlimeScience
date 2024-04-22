using SlimeScience.Characters.Slimes;
using UnityEngine;

namespace SlimeScience.FSM.States.Slimes
{
    public class PatrollState : IBehaviour
    {
        private Slime _slime;

        private float _accelerationTime;

        public PatrollState(Slime slime)
        {
            _slime = slime;
        }

        public void Enter()
        {
            _accelerationTime = 0;
            _slime.Movement.Enable();
            Debug.Log("Patroll");
        }

        public void Exit()
        {
        }

        public bool IsReady()
        {
            return _slime.Movement.IsGettingNewDirection();
        }

        public void Update()
        {
            _accelerationTime += Time.deltaTime;

            if (_accelerationTime >= 2f)
            {
                _slime.Movement.IsGettingNewDirection();
                _slime.Movement.Move();
                _slime.Movement.Disable();
                _accelerationTime = 0;
            }
        }
    }
}