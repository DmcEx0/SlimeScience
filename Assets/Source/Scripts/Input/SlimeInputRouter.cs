using SlimeScience.Util;
using UnityEngine;

namespace SlimeScience.Input
{
    public class SlimeInputRouter : IInputRouter
    {
        private const float RangePosX = 4f;
        private const float RangePosZ = 4f;
        private const float Step = 5f;

        private readonly TargetDetector _detector;

        private Vector3 _newDirection;

        private bool _isEnabled = false;

        public SlimeInputRouter(TargetDetector detectable)
        {
            _detector = detectable;
        }

        public Vector3 GetNewDirection()
        {
            if (_isEnabled == false)
                return Vector3.zero;

            if (_detector.GetTargetIsNearStatus())
            {
                return _detector.GetDirectionFromToTarget(true) * Step;
            }

            return _newDirection;
        }

        public void OnEnable()
        {
            _isEnabled = true;

            float randomPosX = Random.Range(-RangePosX, RangePosX);
            float randomPosZ = Random.Range(-RangePosZ, RangePosZ);

            _newDirection = new Vector3(randomPosX, 0, randomPosZ);
        }

        public void OnDisable()
        {
            _isEnabled = false;

            _newDirection = Vector3.zero;
        }
    }
}