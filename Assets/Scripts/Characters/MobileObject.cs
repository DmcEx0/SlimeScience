using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class MobileObject : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;

    private Movement _movement;

    private StateMachine _stateMachine;

    public Movement Movement => _movement;

    public void Init(StateMachine stateMachine, IInputRouter inputRouter, MobileObjectConfig config)
    {
        _stateMachine = stateMachine;
        _movement = new(_agent, inputRouter);

        Init(config);

        _agent.speed = config.BaseSpeed;
        _agent.angularSpeed = config.AngularSpeed;

        _stateMachine.Start();
    }

    protected void UpdateStateMachine()
    {
        _stateMachine?.Update();
    }

    protected abstract void Init(MobileObjectConfig config);
    protected abstract Dictionary<StatesType, IBehaviour> GetBehaviours();
    protected abstract IBehaviour GetStartState();
}