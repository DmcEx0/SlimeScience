using UnityEngine;

namespace SlimeScience.Input
{
    public class SlimeInputRouter : IInputRouter, IDetectable
    {
        private const float RangePosX = 4f;
        private const float RangePosZ = 4f;

        private Transform _playerTransform;
        private Transform _slimeTransform;

        private float _distanceForRange;

        private Vector3 _newDirection;

        public event System.Action Detected;

        public SlimeInputRouter(Transform playerTransform, float distanceForRange)
        {
            _playerTransform = playerTransform;
            _distanceForRange = distanceForRange;
        }

        public Vector3 GetNewDirection()
        {
            return _newDirection;
        }

        public void OnEnable()
        {
            float randomPosX = Random.Range(-RangePosX, RangePosX);
            float randomPosZ = Random.Range(-RangePosZ, RangePosZ);

            _newDirection = new Vector3(randomPosX, 0, randomPosZ);
        }

        public void OnDisable()
        {
            _newDirection = Vector3.zero;
        }

        private void CheckDistanceToPlayer()
        {
            float distance = (_playerTransform.position - _slimeTransform.position).magnitude;

            if (distance < _distanceForRange)
                Detected?.Invoke();

        }
    }
}