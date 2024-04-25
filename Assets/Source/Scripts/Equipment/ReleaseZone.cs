using SlimeScience.Characters.Playable;
using SlimeScience.Characters.Slimes;
using SlimeScience.InventorySystem;
using UnityEngine;

namespace SlimeScience
{
    public class ReleaseZone : MonoBehaviour
    {
        private Inventory<Slime> _inventory;

        private void Awake()
        {
            _inventory = new(200);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Player>(out Player player))
            {
                player.ReleaseSlimes(transform.position);
            }
        }
    }
}
