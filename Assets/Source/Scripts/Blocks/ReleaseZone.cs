using SlimeScience.Characters.Playable;
using SlimeScience.Characters.Slimes;
using SlimeScience.Configs;
using SlimeScience.InventorySystem;
using System;
using UnityEngine;

namespace SlimeScience.Blocks
{
    public class ReleaseZone : MonoBehaviour
    {
        [SerializeField] private Transform _replasePos;
        [SerializeField] private BlocksConfig _blocksConfig;

        private int _indexCurrentBlock = 0;
        private Inventory<Slime> _inventory;

        public event Action<BlockData, int> OpenedNextBlock;

        public void Init()
        {
            _inventory = new(0);

            OpenNextBlock();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Player>(out Player player))
            {
                foreach (var item in player.ReleaseSlimes(_replasePos.position))
                {
                    _inventory.Add(item);
                }
            }

            if (_inventory.IsFull)
                OpenNextBlock();
        }

        private void OpenNextBlock()
        {
            OpenedNextBlock?.Invoke(_blocksConfig.BlocksData[_indexCurrentBlock], _indexCurrentBlock);

            _indexCurrentBlock++;
            _inventory.Expand((_blocksConfig.BlocksData[_indexCurrentBlock].NeededAmountToOpen));
        }
    }
}
