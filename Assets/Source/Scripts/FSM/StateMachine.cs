using System.Collections.Generic;
using SlimeScience.FSM.States;

namespace SlimeScience.FSM
{
    public class StateMachine
    {
        private Dictionary<StatesType, IState> _states;
        
        private IState _currentState;
        private StatesType _startState;

        public void SetStates(StatesType startState, Dictionary<StatesType, IState> states)
        {
            _states = states;
            _startState = startState;
            
            _currentState = _states[_startState];
        }

        public void Start()
        {
            _currentState.Enter();
        }

        public void Stop()
        {
            _currentState.Exit();
        }

        public void Update()
        {
            _currentState?.Update();
        }

        public void ChangeState(StatesType stateType)
        {
            if (_states.TryGetValue(stateType, out IState state))
            {
                _currentState?.Exit();
                _currentState = state;
                _currentState.Enter();
            }
            else
            {
                UnityEngine.Debug.LogWarning("State not found");
            }
        }
    }
}