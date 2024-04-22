using SlimeScience.Input;
using UnityEngine;
using UnityEngine.AI;

namespace SlimeScience.Characters
{
    public class Movement
    {
        private NavMeshAgent _agent;

        private IInputRouter _inputRouter;

        private Vector3 _newDirection;

        public bool IsMoving
        {
            get => _agent.remainingDistance > _agent.stoppingDistance;
        }

        public Movement(NavMeshAgent agent, IInputRouter controller)
        {
            _agent = agent;
            _inputRouter = controller;
        }

        public void Enable()
        {
            _inputRouter.OnEnable();
        }

        public void Disable()
        {
            _inputRouter.OnDisable();
        }

        public bool IsGettingNewDirection()
        {
            _newDirection = _inputRouter.GetNewDirection();

            if (_newDirection == Vector3.zero)
                return false;
            else
                return true;
        }

        public void Move()
        {
            _agent.SetDestination(_agent.transform.position + _newDirection);
        }
    }
}