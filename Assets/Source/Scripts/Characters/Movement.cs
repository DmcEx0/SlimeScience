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

        public bool IsMoving
        {
            get => _agent.remainingDistance > _agent.stoppingDistance;
        }

        public Movement(NavMeshAgent agent, IInputRouter inputRouter)
        {
            _inputRouter = inputRouter;
            _agent = agent;
        }

        public void Enable()
        {
            _inputRouter.OnEnable();
        }

        public void Disable()
        {
            _inputRouter.OnDisable();
        }

        public void SetMovementSpeed(float speed)
        {
            _agent.speed = speed;
        }

        public void Move()
        {
            Vector3 newDirection = _inputRouter.GetNewDirection();

            _agent.SetDestination(_agent.transform.position + newDirection);
        }
    }
}