using SlimeScience.Characters.Slimes;
using UnityEngine;

namespace SlimeScience.Input
{
    public class SlimeInputRouter : IInputRouter
    {
        private const float RangePosX = 4f;
        private const float RangePosZ = 4f;
        private const float Step = 5f;

        private PlayerDetector _detector;

        private Vector3 _newDirection;

        public SlimeInputRouter(PlayerDetector detectable)
        {
            _detector = detectable;
        }

        public Vector3 GetNewDirection()
        {
            if (_detector.PlayerIsNear())
                return _detector.GetDirectionFromPlayer() * Step;

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
    }
}