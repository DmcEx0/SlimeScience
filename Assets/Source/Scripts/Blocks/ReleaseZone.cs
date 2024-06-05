using SlimeScience.Characters.Playable;
using SlimeScience.Characters.Slimes;
using SlimeScience.Configs;
using SlimeScience.InventorySystem;
using SlimeScience.Money;
using SlimeScience.Saves;
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
        private Wallet _wallet;
        private GameVariables _gameVariables;

        public event Action<BlockData, int> OpenedNextBlock;

        public void Init(Wallet wallet, GameVariables gameVariables)
        {
            _wallet = wallet;
            _gameVariables = gameVariables;
            _inventory = new(0);

            OpenNextBlock();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player player))
            {
                foreach (var item in player.ReleaseSlimes(_replasePos.position))
                {
                    _inventory.Add(item);
                    _wallet.Add(1); // TODO: Add slime price and will use "item.PlaceCost"
                    _gameVariables.AddSlimes(1);
                }

                player.RenderInventory();
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
