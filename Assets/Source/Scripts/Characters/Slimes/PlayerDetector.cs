using SlimeScience.Input;
using UnityEngine;

namespace SlimeScience.Characters.Slimes
{
    public class PlayerDetector : IDetectable
    {
        private Transform _playerTransform;
        private Transform _slimeTransform;
        private float _distanceForFear;

        public PlayerDetector(Transform slimeTransform, Transform playerTransform, float distanceForFear)
        {
            _slimeTransform = slimeTransform;
            _playerTransform = playerTransform;
            _distanceForFear = distanceForFear;
        }

        public bool GetPlayerIsNearStatus()
        {
            float distance = (_playerTransform.position - _slimeTransform.position).magnitude;

            if (distance < _distanceForFear)
            {
                return true;
            }

            return false;
        }

        public Vector3 GetDirectionFromPlayer()
        {
            return (_slimeTransform.position - _playerTransform.position).normalized;
        }
    }
}
