using SlimeScience.Blocks;
using SlimeScience.Configs;
using SlimeScience.Factory;
using SlimeScience.Saves;
using UnityEngine;

namespace SlimeScience.Spawners
{
    public class SlimeSpawner
    {
        private readonly GeneralSlimeFactory _slimeFactory;

        private Transform _playerTransform;
        private GameVariables _gameVariables;

        public SlimeSpawner(GeneralSlimeFactory slimeFactory, GameVariables gameVariables)
        {
            _slimeFactory = slimeFactory;
            _gameVariables = gameVariables;
        }

        public void Init(Transform playerTransform, Transform poolParent, int poolSize)
        {
            _playerTransform = playerTransform;
            _slimeFactory.CreatePool(poolSize, poolParent);
        }

        public void Spawn(BlockData blockData, Block block)
        {
            int slimesToSpawn = blockData.SlimeAmount - _gameVariables.CollectedSlimes;

            for (int i = 0; i < slimesToSpawn; i++)
            {
                float randomPosX = Random.Range(-blockData.MaxRangePosX, blockData.MaxRangePosX);
                float randomPosZ = Random.Range(-blockData.MaxRangePosZ, blockData.MaxRangePosZ);

                Vector3 newPos = new Vector3(randomPosX, 0, randomPosZ) + block.Centre.position;

                _slimeFactory.Get(blockData.SlimesType, _playerTransform, newPos);
            }
        }
    }
}