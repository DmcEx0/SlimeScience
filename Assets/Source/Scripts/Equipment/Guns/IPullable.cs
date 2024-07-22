using UnityEngine;

namespace SlimeScience.Equipment.Guns
{
    public interface IPullable
    {
        public Vector3 Position { get; }

        public void AddForce(Vector3 force);

        public void SetPosition(Vector3 position);

        public void SetActive(bool active);

        public void ResetVelocity();
    }
}
