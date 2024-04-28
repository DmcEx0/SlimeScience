using System;
using System.Collections.Generic;

namespace SlimeScience.FSM
{
    public class StateMachine
    {
        private IBehaviour _currentBehaviour;
        private Dictionary<Type, IBehaviour> _behaviours;

        public StateMachine(IBehaviour startBehaviour, Dictionary<Type, IBehaviour> behaviours)
        {
            Reset();

            _currentBehaviour = startBehaviour;
            _behaviours = behaviours;
        }

        public void Start()
        {
            _currentBehaviour.Enter();
        }

        public void Stop()
        {
            Reset();
        }

        public void Update()
        {
            if (_currentBehaviour == null)
                return;

            _currentBehaviour.Update();

            foreach (var behaviour in _behaviours)
            {
                if (behaviour.Value == _currentBehaviour)
                {
                    continue;
                }

                if (behaviour.Value.IsReady())
                {
                    ChangeState(behaviour.Value);
                    break;
                }
            }
        }

        public void Reset()
        {
            _currentBehaviour?.Exit();

            _currentBehaviour = null;
        }

        public void ChangeState(IBehaviour behaviour)
        {
            Reset();

            _currentBehaviour = behaviour;
            _currentBehaviour.Enter();
        }
    }
}