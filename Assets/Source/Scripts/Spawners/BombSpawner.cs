using SlimeScience.Blocks;
using SlimeScience.Configs;
using SlimeScience.Factory;
using UnityEngine;

namespace SlimeScience
{
    public class BombSpawner
    {
        private GeneralBombFactory _bombFactory;

        private Transform _playerTransform;

        public BombSpawner(GeneralBombFactory bombFactory, Transform parent, int maxBombCount)
        {
            _bombFactory = bombFactory;
            _bombFactory.CreatePool(maxBombCount, parent);
        }

        public void Spawn(BlockData blockData, Block block)
        {
            for (int i = 0; i < blockData.BombAmount; i++)
            {
                float randomPosX = Random.Range(-blockData.MaxRangePosX, blockData.MaxRangePosX);
                float randomPosZ = Random.Range(-blockData.MaxRangePosZ, blockData.MaxRangePosZ);

                Vector3 newPos = new Vector3(randomPosX, 0, randomPosZ) + block.Centre.position;

                _bombFactory.Get(newPos);
            }
        }
    }
}