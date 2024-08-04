using SlimeScience.Input;
using UnityEngine;
using UnityEngine.AI;

namespace SlimeScience.Characters
{
    public class Movement
    {
        private NavMeshAgent _agent;
        private Vector3 _velocity;
        private Vector3 _desiredVelocity;


        private IInputRouter _inputRouter;

        private bool _isEnabled;

        private float _defaultAcceleration;

        public float AgentSpeed => _agent.velocity.magnitude;

        public Movement(NavMeshAgent agent, IInputRouter inputRouter)
        {
            _inputRouter = inputRouter;
            _agent = agent;

            _defaultAcceleration = _agent.acceleration;
        }

        public void Enable()
        {
            _isEnabled = true;
            _inputRouter.OnEnable();

            if (_agent.gameObject.activeSelf && _agent.enabled)
            {
                _agent.isStopped = false;
                _agent.updateRotation = true;
            }
        }

        public void Disable()
        {
            _isEnabled = false;
            _inputRouter.OnDisable();

            if (_agent.enabled && _agent.isOnNavMesh)
            {
                _agent.isStopped = true;
                _agent.updateRotation = false;
            }
        }

        public void SetAcceleration(float accel)
        {
            _agent.acceleration = accel;
        }

        public void SetMovementSpeed(float speed)
        {
            _agent.speed = speed;
        }

        public void SetRotateSpeed(float speed)
        {
            _agent.angularSpeed = speed;
        }

        public void Move(bool isDirectional = true)
        {
            Vector3 newDirection = _inputRouter.GetNewDirection();
            
            if (_agent.isOnNavMesh == false)
            {
                return;
            }

            if (isDirectional)
            {
                _agent.SetDestination(_agent.transform.position + newDirection);
            }
            else
            {
                _agent.SetDestination(newDirection);
            }
        }

        public void Move(Vector3 endPosition)
        {
            if (_agent.isOnNavMesh)
            {
                _agent.SetDestination(endPosition);
            }
        }

        public bool IsEnabled()
        {
            return _agent.isOnNavMesh || _isEnabled || _agent.isActiveAndEnabled;
        }

        public bool IsMoving()
        {
            return _agent.remainingDistance > _agent.stoppingDistance;
        }
    }
}