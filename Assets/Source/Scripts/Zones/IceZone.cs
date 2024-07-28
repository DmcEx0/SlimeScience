using SlimeScience.Characters;
using UnityEngine;

namespace SlimeScience.Zones
{
    public class IceZone : MonoBehaviour
    {
        private const float AccelerationMultiplier = 0.3f;

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.TryGetComponent(out Player player))
            {
                player.SetMovementModifiers(AccelerationMultiplier);
            }
        }

        private void OnTriggerExit(Collider collider)
        {
            if (collider.TryGetComponent(out Player player))
            {
                player.ResetMovementModifiers();
            }
        }
    }
}