using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SlimeScience.Util
{
    public class SlimeObserver
    {
        private const int MaxNumberOfSlimes = 20;
        private const float Radius = 100f;

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

            Collider newSlime = _slimes[Random.Range(0, numberOfSlimes)];
            
            if(newSlime == null)
            {
                Debug.LogError("Slime not found!");
                return false;
            }
            
            targetDetector.SetTargetTransform(newSlime.transform);
            return true;
        }
    }
}