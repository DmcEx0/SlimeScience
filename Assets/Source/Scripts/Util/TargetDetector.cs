using SlimeScience.Input;
using UnityEngine;

namespace SlimeScience.Util
{
    public class TargetDetector : IDetectable
    {
        private readonly float _distanceForActivate;

        private Transform _targetTransform;
        private Transform _parentTransform;

        public TargetDetector(float distanceForActivate)
        {
            _distanceForActivate = distanceForActivate;
        }

        public void SetParentTransforms(Transform parentTransform)
        {
            _parentTransform = parentTransform;
        }

        public void SetTargetTransform(Transform targetTransform)
        {
            _targetTransform = targetTransform;
        }

        public bool GetTargetIsNearStatus()
        {
            if(HasTransforms() == false)
            {
                return HasTransforms();
            }
            
            float distance = (_targetTransform.position - _parentTransform.position).magnitude;

            if (distance < _distanceForActivate)
            {
                return true;
            }

            return false;
        }

        public Vector3 GetDirectionFromToTarget(bool isFromTarget)
        {
            if(HasTransforms() == false)
            {
                return Vector3.zero;
            }
            
            if (isFromTarget)
            {
                return (_parentTransform.position - _targetTransform.position).normalized;
            }

            // return (_targetTransform.position - _parentTransform.position).normalized;
            return _targetTransform.position;
        }

        private bool HasTransforms()
        {
            if (_parentTransform == null)
            {
                return false;
            }

            if (_targetTransform == null || _targetTransform.gameObject.activeInHierarchy == false)
            {
                return false;
            }

            return true;
        }
    }
}