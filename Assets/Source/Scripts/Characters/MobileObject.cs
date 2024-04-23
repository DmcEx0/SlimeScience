using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using SlimeScience.FSM;
using SlimeScience.Input;
using SlimeScience.Configs;
using SlimeScience.FSM.States;

namespace SlimeScience.Characters
{
    public abstract class MobileObject : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;

        private Movement _movement;

        private StateMachine _stateMachine;

        private IInputRouter _inputRouter;

        public Movement Movement => _movement;

        public void Init(StateMachine stateMachine, IInputRouter inputRouter, MobileObjectConfig config)
        {
            _stateMachine = stateMachine;

            Init(config);

            _movement = new(_agent, inputRouter);
            _movement.SetMovementSpeed(config.BaseSpeed);

            _agent.angularSpeed = config.AngularSpeed;

            _stateMachine.Start();
        }

        protected void UpdateStateMachine()
        {
            _stateMachine?.Update();
        }

        protected abstract void Init(MobileObjectConfig config);
    }
}