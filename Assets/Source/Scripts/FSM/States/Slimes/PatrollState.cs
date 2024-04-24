using SlimeScience.Characters.Slimes;
using SlimeScience.Input;
using UnityEngine;

namespace SlimeScience.FSM.States.Slimes
{
    public class PatrollState : IBehaviour
    {
        private Slime _slime;
        private IDetectable _detector;

        private float _accelerationTime;
        private readonly float _delay = 2f;

        public PatrollState(Slime slime, IDetectable detector)
        {
            _slime = slime;
            _detector = detector;
        }

        public void Enter()
        {
            _accelerationTime = _delay;
            Debug.Log("Patroll");

            _slime.Movement.SetMovementSpeed(_slime.BaseSpeed);
        }

        public void Exit()
        {
        }

        public bool IsReady()
        {
            return _detector.PlayerIsNear() == false;
        }

        public void Update()
        {
            _slime.Movement.Disable();

            _accelerationTime += Time.deltaTime;

            if (_accelerationTime >= _delay)
            {
                _slime.Movement.Enable();
                _slime.Movement.Move();
                _accelerationTime = 0;
            }
        }
    }
}