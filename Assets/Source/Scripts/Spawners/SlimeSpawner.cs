using SlimeScience.Configs;
using SlimeScience.Factory;
using Unity.AI.Navigation;
using UnityEngine;

namespace SlimeScience.Spawners
{
    public class SlimeSpawner
    {
        private GeneralSlimeFactory _slimeFactory;

        private Transform _playerTransform;

        public SlimeSpawner(GeneralSlimeFactory slimeFactory)
        {
            _slimeFactory = slimeFactory;
        }

        public void Init(Transform playerTransform)
        {
            _playerTransform = playerTransform;
        }

        public void Spawn(BlockData blockData)
        {
            for (int i = 0; i < blockData.SlimeAmount; i++)
            {
                float randomPosX = Random.Range(-blockData.MaxRangePosX, blockData.MaxRangePosX);
                float randomPosZ = Random.Range(-blockData.MaxRangePosZ, blockData.MaxRangePosZ);

                Vector3 newPos = new Vector3(randomPosX, 0, randomPosZ) + blockData.Block.transform.position;

                var newSlime = _slimeFactory.Get(_playerTransform);
                newSlime.transform.position = newPos;
            }
        }
    }
}