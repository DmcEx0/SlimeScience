using System;
using System.Collections.Generic;
using SlimeScience.Characters.Slimes;
using UnityEngine;


namespace SlimeScience.Equipment.Guns
{
    public class SlimeFinder
    {
        private const int DefaultCapacity = 10;

        private LayerMask _slimeLayerMask;
        private List<Slime> _slimes = new List<Slime>();
        private Collider[] _colliders = new Collider[DefaultCapacity];

        public SlimeFinder(LayerMask slimeLayerMask)
        {
            _slimeLayerMask = slimeLayerMask;
        }

        public List<Slime> GetSlimes(Transform transform, float radius, float angle)
        {
            _slimes.Clear();

            int numColliders = Physics.OverlapSphereNonAlloc(transform.position, radius, _colliders, _slimeLayerMask);

            for (int i = 0; i < numColliders; i++)
            {
                var collider = _colliders[i];

                Vector3 direction = collider.transform.position - transform.position;
                if (Vector3.Angle(direction, transform.forward) < angle)
                {
                    if (collider.TryGetComponent(out Slime slime))
                    {
                        _slimes.Add(slime);
                    }
                }
            }

            Array.Clear(_colliders, 0, numColliders);

            return _slimes;
        }
    }
}