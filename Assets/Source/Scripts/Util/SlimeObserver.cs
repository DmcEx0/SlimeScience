using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SlimeScience.Util
{
    public class SlimeObserver
    {
        private const int MaxNumberOfSlimes = 20;
        private const float Radius = 50f;

        private const int SlimeLayer = 6;

        private Transform _vacuumingTransform;
        private Collider[] _slimes;

        public SlimeObserver(Transform vacuumingTransform)
        {
            _vacuumingTransform = vacuumingTransform;
            _slimes = new Collider[MaxNumberOfSlimes];
        }

        public bool TryGetNewSlime(TargetDetector targetDetector)
        {
            if (_slimes.Length != 0)
            {
                Array.Clear(_slimes, 0, _slimes.Length);
            }

            int numberOfSlimes = Physics.OverlapSphereNonAlloc(_vacuumingTransform.position, Radius, _slimes, 1 << SlimeLayer);

            Collider currentSlime = _slimes[Random.Range(0, numberOfSlimes)];
            
            if(currentSlime == null)
            {
                Debug.LogError("Slime not found!");
                return false;
            }
            
            targetDetector.SetTargetTransform(currentSlime.transform);
            return true;
        }
    }
}