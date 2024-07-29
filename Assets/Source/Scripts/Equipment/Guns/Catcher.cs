using System;
using UnityEngine;

namespace SlimeScience.Equipment.Guns
{
    public class Catcher
    {
        public Action<IPullable> Caught;

        public void Absorb(IPullable pullable, Vector3 catchPoint, float force)
        {
            var forceDirection = (catchPoint - pullable.Position).normalized;
            var correctDirection = new Vector3(forceDirection.x, 0, forceDirection.z);

            pullable.AddForce(correctDirection * force);
        }

        public void Catch(IPullable pullable, Vector3 catchPoint)
        {
            Caught?.Invoke(pullable);
            pullable.SetActive(false);
            pullable.SetPosition(catchPoint);
        }
    }
}
