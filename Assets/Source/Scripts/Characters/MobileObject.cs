using UnityEngine;
using UnityEngine.AI;
using SlimeScience.FSM;
using SlimeScience.Input;
using SlimeScience.Configs;

namespace SlimeScience.Characters
{
    public abstract class MobileObject : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;

        private Movement _movement;

        private StateMachine _stateMachine;

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

        public void Enable()
        {
            _agent.enabled = true;
            _movement.Enable();
            _stateMachine.Start();
        }

        public void Disable()
        {
            _movement.Disable();
            _stateMachine.Stop();
            _agent.enabled = false;
        }

        protected void UpdateStateMachine()
        {
            _stateMachine?.Update();
        }

        protected abstract void Init(MobileObjectConfig config);

        protected virtual void SetRigidbodySetting(Rigidbody rigidbody)
        {
            rigidbody.interpolation = RigidbodyInterpolation.None;
            rigidbody.useGravity = false;
            rigidbody.freezeRotation = true;
        }
    }
}