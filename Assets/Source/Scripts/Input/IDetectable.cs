using UnityEngine;

namespace SlimeScience.Input
{
    public interface IDetectable
    {
        public bool GetTargetIsNearStatus();

        public bool HasTargetTransforms();
    }
}