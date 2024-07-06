using SlimeScience.Input;
using UnityEngine;
using UnityEngine.AI;

namespace SlimeScience.Characters
{
    public class Movement
    {
        private NavMeshAgent _agent;

        private IInputRouter _inputRouter;

        private bool _isEnabled;

        public float AgentSpeed => _agent.velocity.magnitude;

        public Movement(NavMeshAgent agent, IInputRouter inputRouter)
        {
            _inputRouter = inputRouter;
            _agent = agent;
        }

        public void Enable()
        {
            _isEnabled = true;
            _inputRouter.OnEnable();

            if (_agent.enabled)
            {
                _agent.isStopped = false;
                _agent.updateRotation = true;
            }
        }

        public void Disable()
        {
            _isEnabled = false;
            _inputRouter.OnDisable();

            if (_agent.enabled)
            {
                _agent.isStopped = true;
                _agent.updateRotation = false;
            }
        }

        public void SetMovementSpeed(float speed)
        {
            _agent.speed = speed;
        }

        public void Move()
        {
            Vector3 newDirection = _inputRouter.GetNewDirection();

            if (_agent.isOnNavMesh)
            {
                _agent.SetDestination(_agent.transform.position + newDirection);
            }
        }

        public void Move(Vector3 endPosition)
        {
            _agent.SetDestination(endPosition);
        }

        public bool IsEnabled()
        {
            return _agent.isOnNavMesh || _isEnabled;
        }

        public bool IsMoving()
        {
            if (_agent.isActiveAndEnabled)
            {
                return _agent.remainingDistance > _agent.stoppingDistance;
            }

            return false;
        }
    }
}