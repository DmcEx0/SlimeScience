using System;
using System.Collections.Generic;
using UnityEngine;

namespace SlimeScience.Equipment.Guns
{
    public class Finder
    {
        private const int DefaultCapacity = 50;

        private LayerMask _slimeLayerMask;
        private List<IPullable> _slimes = new List<IPullable>();
        private Collider[] _colliders = new Collider[DefaultCapacity];

        public Finder(LayerMask slimeLayerMask)
        {
            _slimeLayerMask = slimeLayerMask;
        }

        public List<IPullable> GetPullables(Transform transform, float radius, float angle)
        {
            _slimes.Clear();

            int numColliders = Physics.OverlapSphereNonAlloc(transform.position, radius, _colliders, _slimeLayerMask);

            for (int i = 0; i < numColliders; i++)
            {
                var collider = _colliders[i];

                Vector3 direction = collider.transform.position - transform.position;

                if (Vector3.Angle(direction, transform.forward) < angle)
                {
                    if (collider.TryGetComponent(out IPullable pullable))
                    {
                        _slimes.Add(pullable);
                    }
                }
            }

            Array.Clear(_colliders, 0, numColliders);

            return _slimes;
        }
    }
}