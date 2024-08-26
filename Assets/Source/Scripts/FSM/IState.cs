namespace SlimeScience.FSM
{
    public interface IState
    {
        public void Enter();

        public void Exit();

        public void Update();
    }
}
