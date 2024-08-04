using SlimeScience.Util;
using UnityEngine;

namespace SlimeScience.Input
{
    public class VacuumingSupportInputRouter : IInputRouter
    {
        private readonly TargetDetector _targetDetector;
        private readonly SlimeObserver _slimeObserver;
        private readonly Transform _releaseZoneTransform;

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
            
            return targetDir;
        }

        public void OnEnable()
        {
            _isEnabled = true;
            
            if(_slimeObserver.TryGetNewSlime(out Collider newSlime))
            {
                _targetDetector.SetTargetTransform(newSlime.transform);
            }
        }

        public void OnDisable()
        {
            _isEnabled = false;
        }
    }
}