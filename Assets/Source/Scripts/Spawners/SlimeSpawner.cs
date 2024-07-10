using SlimeScience.Blocks;
using SlimeScience.Configs;
using SlimeScience.Factory;
using UnityEngine;

namespace SlimeScience.Spawners
{
    public class SlimeSpawner
    {
        private readonly GeneralSlimeFactory _slimeFactory;

        private Transform _playerTransform;

        public SlimeSpawner(GeneralSlimeFactory slimeFactory)
        {
            _slimeFactory = slimeFactory;
        }

        public void Init(Transform playerTransform, Transform poolParent, int poolSize)
        {
            _playerTransform = playerTransform;
            _slimeFactory.CreatePool(poolSize, poolParent);
        }

        public void Spawn(BlockData blockData, Block block)
        {
            for (int i = 0; i < blockData.SlimeAmount; i++)
            {
                float randomPosX = Random.Range(-blockData.MaxRangePosX, blockData.MaxRangePosX);
                float randomPosZ = Random.Range(-blockData.MaxRangePosZ, blockData.MaxRangePosZ);

                Vector3 newPos = new Vector3(randomPosX, 0, randomPosZ) + block.Centre.position;

                _slimeFactory.Get(_playerTransform, newPos);
            }
        }
    }
}