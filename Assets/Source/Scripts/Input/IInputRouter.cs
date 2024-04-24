using UnityEngine;

namespace SlimeScience.Input
{
    public interface IInputRouter
    {
        public void OnEnable();
        public void OnDisable();
        public Vector3 GetNewDirection();
    }
}