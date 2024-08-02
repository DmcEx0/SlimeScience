using SlimeScience.Util;
using UnityEngine;

namespace SlimeScience.Input
{
    public class VacuumingSupportInputRouter : IInputRouter
    {
        private const float Step = 3f;

        private readonly TargetDetector _targetDetector;
        private readonly SlimeObserver _slimeObserver;
        private readonly Transform _releaseZoneTransform;

        private Vector3 _newDirection;

        private bool _isEnabled;

        public VacuumingSupportInputRouter(TargetDetector targetDetector, SlimeObserver slimeObserver, Transform releaseZoneTransform)
        {
            _targetDetector = targetDetector;
            _slimeObserver = slimeObserver;
            _releaseZoneTransform = releaseZoneTransform;
        }

        public Vector3 GetNewDirection()
        {
            if (_isEnabled == false)
                return Vector3.zero;
            
            var targetDir = _targetDetector.GetDirectionFromToTarget(false);
            
            if(targetDir == Vector3.zero)
            {
                OnEnable();
            }

            return targetDir;
        }

        public void OnEnable()
        {
            _isEnabled = true;
            
            if(_slimeObserver.TryGetNewSlime(out Collider newSlime))
            {
                _targetDetector.SetTargetTransform(newSlime.transform);
            }
            else
            {
                _targetDetector.SetTargetTransform(_releaseZoneTransform);
            }
        }

        public void OnDisable()
        {
            _isEnabled = false;

            _newDirection = Vector3.zero;
        }
    }
}