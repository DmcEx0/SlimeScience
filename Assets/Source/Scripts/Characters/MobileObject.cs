using UnityEngine;
using UnityEngine.AI;
using SlimeScience.FSM;
using SlimeScience.Input;
using SlimeScience.Configs;
using SlimeScience.Util;

namespace SlimeScience.Characters
{
    public abstract class MobileObject : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private Animator _animator;
        
        private StateMachine _stateMachine;

        public Movement Movement { get; private set; }

        public void Init(StateMachine stateMachine, IInputRouter inputRouter, MobileObjectConfig config)
        {
            _stateMachine = stateMachine;

            Init(config);

            Movement = new(_agent, inputRouter);

            SetMovementStats(config);

            Enable();
        }

        public void Enable()
        {
            _agent.enabled = true;
            Movement.Enable();
            _stateMachine.Start();
        }

        public void Disable()
        {
            _stateMachine.Stop();
            Movement.Disable();
            _agent.enabled = false;
        }

        public void ChangeAgent(AgentTypeIds agentTypeId)
        {
            if (_agent.hasPath)
            {
                _agent.ResetPath();
            }

            _agent.agentTypeID = (int)agentTypeId;
        }

        public void SetMovementStats(MobileObjectConfig config)
        {
            Movement.SetMovementSpeed(config.BaseSpeed);
            Movement.SetRotateSpeed(config.AngularSpeed);
            Movement.SetAcceleration(config.Acceleration);
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
        
        public void ChangeAnimationState<T>(int hashName, T value)
        {
            switch (value)
            {
                case float f:
                    _animator.SetFloat(hashName, f);
                    break;
                case int i:
                    _animator.SetInteger(hashName, i);
                    break;
                case bool b:
                    _animator.SetBool(hashName, b);
                    break;
            }
        }
    }
}