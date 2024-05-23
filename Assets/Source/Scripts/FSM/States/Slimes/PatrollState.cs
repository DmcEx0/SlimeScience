using SlimeScience.Characters.Slimes;
using SlimeScience.Input;
using SlimeScience.Utils;
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
            _slime.ChangeAnimationState(AnimationHashNames.Speed, 0f);

            DefineRandomDelay();
        }

        public void Exit()
        {
            _slime.Movement.Enable();
        }

        public bool IsReady()
        {
            return _detector.PlayerIsNear() == false;
        }

        public void Update()
        {
            _accelerationTime += Time.deltaTime;

            if (_accelerationTime >= _currentDelay)
            {
                Debug.Log("!!!");
                _slime.ChangeAnimationState(AnimationHashNames.Speed, _slime.Movement.AgentSpeed);

                _slime.Movement.Enable();
                _slime.Movement.Move();
                _accelerationTime = 0;
                DefineRandomDelay();
            }
            
            if (_slime.Movement.IsPlaceable && _slime.Movement.IsMoving == false)
            {
                Debug.Log("Disable");
                _slime.ChangeAnimationState(AnimationHashNames.Speed, 0f);
                _slime.Movement.Disable();
            }
        }

        private void DefineRandomDelay()
        {
            _currentDelay = Random.Range(_minDelay, _maxDelay);
        }
    }
}