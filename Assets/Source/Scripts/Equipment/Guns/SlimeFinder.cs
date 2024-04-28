using System.Collections.Generic;
using SlimeScience.Characters.Slimes;
using UnityEngine;

namespace SlimeScience.Equipment.Guns
{
    public class SlimeFinder
    {
        private LayerMask _slimeLayerMask;

        public SlimeFinder(LayerMask slimeLayerMask)
        {
            _slimeLayerMask = slimeLayerMask;
        }

        public List<Slime> GetSlimes(Transform transform, float radius, float angle)
        {
            List<Slime> slimes = new List<Slime>();
            Collider[] cols = Physics.OverlapSphere(transform.position, radius, _slimeLayerMask);

            foreach (Collider collider in cols)
            {
                Vector3 characterToCollider = (collider.transform.position - transform.position).normalized;
                float dot = Vector3.Dot(characterToCollider, transform.forward);

                if (dot >= Mathf.Cos(angle * Mathf.Deg2Rad))
                {
                    if (collider.TryGetComponent(out Slime slime))
                    {
                        slimes.Add(slime);
                    }
                }
            }

            return slimes;
        }
    }
}