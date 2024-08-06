using SlimeScience.Characters;
using SlimeScience.Configs;
using SlimeScience.InventorySystem;
using SlimeScience.Money;
using SlimeScience.Saves;
using System;
using System.Collections.Generic;
using SlimeScience.Characters.Ship;
using SlimeScience.Util;
using UnityEngine;

namespace SlimeScience.Blocks
{
    public class ReleaseZone : MonoBehaviour
    {
        [SerializeField] private Collider _collider;
        
        private BlocksConfig _blocksConfig;

        private int _indexCurrentBlock = 0;
        private Inventory<Slime> _inventory;
        private Wallet _wallet;
        private GameVariables _gameVariables;

        public event Action<BlockData, int> OpenedNextBlock;
        public event Action PlayerReleased;
        public event Action LastBlockClosed;

        public Collider Collider => _collider;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ISeekable seeker))
            {
                List<Slime> releaseSimes = seeker.ReleaseSlimes();
                
                if(releaseSimes.Count != 0)
                {
                    if(seeker is Player || seeker is Ship)
                    {
                        SoundsManager.PlayUnloadSlime();
                        PlayerReleased?.Invoke();
                    }
                
                    foreach (var item in releaseSimes)
                    {
                        _inventory.Add(item);
                        _wallet.Add(1); // TODO: Add slime price and will use "item.PlaceCost"
                        _gameVariables.AddSlimes(1);
                        _gameVariables.AddCollectedSlimes(1);
                    }
                }
            }
            
            if (_inventory.IsFull)
            {
                _gameVariables.ResetCollectSlimes();
                _gameVariables.IncreaseRoomIndex();
                OpenNextBlock();
            }
        }

        public void Init(Wallet wallet, GameVariables gameVariables, BlocksConfig blocksConfig)
        {
            _wallet = wallet;
            _gameVariables = gameVariables;
            _indexCurrentBlock = gameVariables.RoomIndex;
            _inventory = new(0);
            _blocksConfig = blocksConfig;

            OpenNextBlock(true);
        }
        
        private void OpenNextBlock(bool isFirstLaunch = false)
        { 
            if(_indexCurrentBlock > _blocksConfig.BlocksData.Count - 1)
            {
                LastBlockClosed?.Invoke();
                return;
            }
            
            if(_indexCurrentBlock != 0 && isFirstLaunch == false)
            {
                SoundsManager.PlayLevelOpened();
            }

            OpenedNextBlock?.Invoke(_blocksConfig.BlocksData[_indexCurrentBlock], _indexCurrentBlock);

            int slimesCount = _gameVariables.CollectedSlimes;

            if (_inventory.MaxItems == 0)
            {
                for (int i = 0; i < _indexCurrentBlock; i++)
                {
                    _inventory.Expand(_blocksConfig.BlocksData[i].NeededAmountToOpen);
                    slimesCount += _blocksConfig.BlocksData[i].NeededAmountToOpen;
                }
            }

            int openAmount = _blocksConfig.BlocksData[_indexCurrentBlock].NeededAmountToOpen;
            _inventory.Expand(openAmount);

            _inventory.Fill(slimesCount);

            _indexCurrentBlock++;
            _gameVariables.SetSlimesGoal(openAmount);
        }
    }
}
