using System;
using SlimeScience.Characters;
using SlimeScience.Input;
using SlimeScience.Util;

namespace SlimeScience.FSM.States.Slimes
{
    public class FearState : IState
    {
        private Slime _slime;
        private IDetectable _detector;

        private Action<StatesType> _changeState;

        public FearState(Action<StatesType> changeState, Slime slime, IDetectable detector)
        {
            _changeState = changeState;
            _slime = slime;
            _detector = detector;
        }

        public void Enter()
        {
            _slime.Movement.Enable();
            _slime.Movement.SetMovementSpeed(_slime.FearSpeed);
        }

        public void Exit()
        {
            _slime.Movement.Disable();
        }

        public void Update()
        {
            _slime.ChangeAnimationState(AnimationHashNames.Speed, _slime.Movement.AgentSpeed);

            if (_detector.GetTargetIsNearStatus() == false)
            {
                _changeState?.Invoke(StatesType.SlimeIdle);
            }

            if (_slime.Movement.IsEnabled())
            {
                _slime.Movement.Move();
            }
        }
    }
}