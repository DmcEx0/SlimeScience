using SlimeScience.Util;
using UnityEngine;

namespace SlimeScience.Input
{
    public class VacuumingSupportInputRouter : IInputRouter
    {
        private const float Step = 1f;

        private readonly TargetDetector _targetDetector;
        private readonly SlimeObserver _slimeObserver;

        private Vector3 _newDirection;

        private bool _isEnabled;

        public VacuumingSupportInputRouter(TargetDetector targetDetector, SlimeObserver slimeObserver)
        {
            _targetDetector = targetDetector;
            _slimeObserver = slimeObserver;
        }

        public Vector3 GetNewDirection()
        {
            if (_isEnabled == false)
                return Vector3.zero;

            return _targetDetector.GetDirectionFromToTarget(false) * Step;
        }

        public void OnEnable()
        {
            _isEnabled = true;
            _slimeObserver.ChangeNewSlime(_targetDetector);
        }

        public void OnDisable()
        {
            _isEnabled = false;

            _newDirection = Vector3.zero;
        }
    }
}