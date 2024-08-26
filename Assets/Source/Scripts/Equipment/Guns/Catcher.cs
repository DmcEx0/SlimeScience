using System;
using SlimeScience.Characters;
using UnityEngine;

namespace SlimeScience.Equipment.Guns
{
    public class Catcher
    {
        public event Action<IPullable> Caught;

        public event Action<IPullable> Pulled;

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

        public void Pull(Slime slime, int size)
        {
            slime.Pull(size);
            Pulled?.Invoke(slime);
        }
    }
}
