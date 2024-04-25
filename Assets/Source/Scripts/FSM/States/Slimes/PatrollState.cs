using SlimeScience.Characters.Slimes;
using SlimeScience.Input;
using UnityEngine;

namespace SlimeScience.FSM.States.Slimes
{
    public class PatrollState : IBehaviour
    {
        private readonly float _minDelay = 1f;
        private readonly float _maxDelay = 3f;

        private readonly Slime _slime;
        private readonly IDetectable _detector;

        private float _accelerationTime;
        private float _currentDelay;

        public PatrollState(Slime slime, IDetectable detector)
        {
            _slime = slime;
            _detector = detector;
        }

        public void Enter()
        {
            DefineRandomDelay();
            _accelerationTime = _currentDelay;

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

            if (_accelerationTime >= _currentDelay)
            {
                _slime.Movement.Enable();
                _slime.Movement.Move();
                _accelerationTime = 0;
                DefineRandomDelay();
            }
        }

        private void DefineRandomDelay()
        {
            _currentDelay = Random.Range(_minDelay, _maxDelay);
        }
    }
}