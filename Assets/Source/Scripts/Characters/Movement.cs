using SlimeScience.Characters.Slimes;
using SlimeScience.Input;
using UnityEngine;
using UnityEngine.AI;

namespace SlimeScience.Characters
{
    public class Movement
    {
        private NavMeshAgent _agent;

        private IInputRouter _inputRouter;

        private bool _isEnabled = false;

        public bool IsPlaceable => _agent.isOnNavMesh || _isEnabled;
        public bool IsMoving => _agent.remainingDistance > _agent.stoppingDistance;

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
        }

        public void Disable()
        {
            _isEnabled = false;
            _inputRouter.OnDisable();
        }

        public void SetMovementSpeed(float speed)
        {
            _agent.speed = speed;
        }

        public void Move()
        {
            if (IsPlaceable == false)
            {
                return;
            }
            
            if(IsMoving == false)
            {
                _agent.isStopped = true;
                _agent.updateRotation = false;
            }
            
            _agent.isStopped = false;
            _agent.updateRotation = true;

            Vector3 newDirection = _inputRouter.GetNewDirection();

            _agent.SetDestination(_agent.transform.position + newDirection);
        }
    }
}