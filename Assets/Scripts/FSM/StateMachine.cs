using System;
using System.Collections.Generic;

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

    public void Update()
    {
        _currentBehaviour?.Update();

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