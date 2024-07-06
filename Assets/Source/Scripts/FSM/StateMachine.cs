using System.Collections.Generic;
using SlimeScience.FSM.States;

namespace SlimeScience.FSM
{
    public class StateMachine
    {
        private Dictionary<StatesType, IState> _states;
        
        private IState _currentState;
        private StatesType _startState;

        private bool _isStoped;

        public void SetStates(StatesType startState, Dictionary<StatesType, IState> states)
        {
            _states = states;
            _startState = startState;
            
            _currentState = _states[_startState];
        }

        public void Start()
        {
            _currentState.Enter();
            _isStoped = false;
        }

        public void Stop()
        {
            _currentState.Exit();
            _isStoped = true;
        }

        public void Update()
        {
            if(_isStoped)
            {
                return;
            }
            
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