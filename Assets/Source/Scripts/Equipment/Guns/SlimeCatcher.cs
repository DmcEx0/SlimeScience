using SlimeScience.Characters.Slimes;
using System;
using UnityEngine;

namespace SlimeScience.Equipment.Guns
{
    public class SlimeCatcher
    {
        public Action<Slime> Caught;

        public void AbsorbSlime(Slime slime, Vector3 catchPoint, float force)
        {
            var forceDirection = (catchPoint - slime.transform.position).normalized;
            slime.AddForce(forceDirection * force);
        }

        public void CatchSlime(Slime slime, Vector3 catchPoint)
        {
            Caught?.Invoke(slime);
            slime.gameObject.SetActive(false);
            slime.transform.position = catchPoint;
        }
    }
}
