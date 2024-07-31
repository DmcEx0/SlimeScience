using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SlimeScience.Util
{
    public class SlimeObserver
    {
        private const int MaxNumberOfSlimes = 20;
        private const float Radius = 1000f;

        private const int SlimeLayer = 6;

        private readonly Transform _vacuumingTransform;
        private readonly Collider[] _slimes;

        public SlimeObserver(Transform vacuumingTransform)
        {
            _vacuumingTransform = vacuumingTransform;
            _slimes = new Collider[MaxNumberOfSlimes];
        }

        public bool TryGetNewSlime(out Collider newSlime)
        {
            if (_slimes.Length != 0)
            {
                Array.Clear(_slimes, 0, _slimes.Length);
            }

            int numberOfSlimes = Physics.OverlapSphereNonAlloc(_vacuumingTransform.position, Radius, _slimes, 1 << SlimeLayer);

            newSlime = _slimes[Random.Range(0, numberOfSlimes)];
            
            if (numberOfSlimes == 0)
            {
                return false;
            }
            
            return true;
        }
    }
}