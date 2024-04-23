using SlimeScience.Characters.Slimes;
using SlimeScience.Input;

namespace SlimeScience.FSM.States.Slimes
{
    public class FearState : IBehaviour
    {
        private Slime _slime;
        IDetectable _detector;

        public FearState(Slime slime, IDetectable detector)
        {
            _slime = slime;
            _detector = detector;
        }

        public void Enter()
        {
            UnityEngine.Debug.Log("Fear");
            _slime.Movement.SetMovementSpeed(_slime.FearSpeed);
        }

        public void Exit()
        {
        }

        public bool IsReady()
        {
            return _detector.PlayerIsNear();
        }

        public void Update()
        {
            _slime.Movement.Move();
        }
    }
}