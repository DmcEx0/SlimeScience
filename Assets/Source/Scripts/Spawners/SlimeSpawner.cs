using SlimeScience.Blocks;
using SlimeScience.Characters;
using SlimeScience.Configs;
using SlimeScience.Factory;
using SlimeScience.Saves;
using UnityEngine;

namespace SlimeScience.Spawners
{
    public class SlimeSpawner
    {
        private const int BigModifier = 3;
        private const int TeleportModifier = 2;

        private readonly GeneralSlimeFactory _slimeFactory;

        private Transform _playerTransform;
        private GameVariables _gameVariables;

        public SlimeSpawner(GeneralSlimeFactory slimeFactory, GameVariables gameVariables)
        {
            _slimeFactory = slimeFactory;
            _gameVariables = gameVariables;
        }

        public void Init(Transform playerTransform, Transform poolParent)
        {
            _playerTransform = playerTransform;
            _slimeFactory.CreatePool(poolParent);
        }

        public void Spawn(BlockData blockData, Block block)
        {
            int collectedSlimes = _gameVariables.CollectedSlimes;
            int slimeAmount = blockData.SlimeAmount;
            int slimesToSpawn = slimeAmount - collectedSlimes;

            if (blockData.SlimesType == SlimeType.Big)
            {
                slimesToSpawn = slimeAmount * BigModifier - collectedSlimes;
                slimesToSpawn /= BigModifier;
            }
            else if (blockData.SlimesType == SlimeType.Teleport)
            {
                slimesToSpawn = slimeAmount * TeleportModifier - collectedSlimes;
                slimesToSpawn /= TeleportModifier;
            }

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