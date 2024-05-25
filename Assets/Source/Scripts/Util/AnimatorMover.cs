using UnityEngine;
using UnityEngine.AI;

namespace SlimeScience
{
    public class AnimatorMover : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private NavMeshAgent _agent;

        private void OnAnimatorMove()
        {
            Vector3 position = _animator.rootPosition;
            position.y = _agent.nextPosition.y;
            transform.position = position;
            _agent.nextPosition = transform.position;
        }
    }
}