using SlimeScience.Characters;
using System;
using UnityEngine;

namespace SlimeScience.Equipment.Guns
{
    public class Catcher
    {
        public Action<IPullable> Caught;

        public void Absorb(IPullable slime, Vector3 catchPoint, float force)
        {
            var forceDirection = (catchPoint - slime.Position).normalized;
            var correctDirection = new Vector3(forceDirection.x, 0, forceDirection.z);

            slime.AddForce(correctDirection * force);
        }

        public void Catch(IPullable slime, Vector3 catchPoint)
        {
            Caught?.Invoke(slime);
            slime.SetActive(false);
            slime.SetPosition(catchPoint);
        }
    }
}
