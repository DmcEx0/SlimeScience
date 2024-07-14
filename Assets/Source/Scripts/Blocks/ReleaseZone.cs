using SlimeScience.Characters;
using SlimeScience.Configs;
using SlimeScience.InventorySystem;
using SlimeScience.Money;
using SlimeScience.Saves;
using System;
using System.Collections.Generic;
using SlimeScience.Util;
using UnityEngine;

namespace SlimeScience.Blocks
{
    public class ReleaseZone : MonoBehaviour
    {
        private BlocksConfig _blocksConfig;

        private int _indexCurrentBlock = 0;
        private Inventory<Slime> _inventory;
        private Wallet _wallet;
        private GameVariables _gameVariables;

        public event Action<BlockData, int> OpenedNextBlock;
        public event Action PlayerReleased;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ISeekable seeker))
            {
                List<Slime> releaseSimes = seeker.ReleaseSlimes();
                
                if(releaseSimes.Count != 0)
                {
                    foreach (var item in releaseSimes)
                    {
                        _inventory.Add(item);
                        _wallet.Add(1); // TODO: Add slime price and will use "item.PlaceCost"
                        _gameVariables.AddSlimes(1);
                    }

                    if (seeker is Player)
                    {
                        SoundsManager.PlayUnloadSlime();
                        PlayerReleased?.Invoke();
                    }
                }
            }

            if (_inventory.IsFull)
                OpenNextBlock();
        }

        public void Init(Wallet wallet, GameVariables gameVariables, BlocksConfig blocksConfig)
        {
            _wallet = wallet;
            _gameVariables = gameVariables;
            _inventory = new(0);
            _blocksConfig = blocksConfig;

            OpenNextBlock();
        }
        
        private void OpenNextBlock()
        {
            if(_indexCurrentBlock != 0)
                SoundsManager.PlayLevelOpened();
            
            if(_indexCurrentBlock + 1 >=_blocksConfig.BlocksData.Count)
            {
                return;
            }
            
            OpenedNextBlock?.Invoke(_blocksConfig.BlocksData[_indexCurrentBlock], _indexCurrentBlock);

            _indexCurrentBlock++;
            _inventory.Expand((_blocksConfig.BlocksData[_indexCurrentBlock].NeededAmountToOpen));
        }
    }
}
