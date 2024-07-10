using SlimeScience.Blocks;
using SlimeScience.Configs;
using SlimeScience.Factory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SlimeScience
{
    public class BombSpawner
    {
        private GeneralBombFactory _bombFactory;

        private Transform _playerTransform;

        public BombSpawner(GeneralBombFactory bombFactory)
        {
            _bombFactory = bombFactory;
        }

        public void Spawn(BlockData blockData, Block block)
        {
            Debug.Log("Bomb spawner called");

            for (int i = 0; i < blockData.SlimeAmount; i++)
            {
                float randomPosX = Random.Range(-blockData.MaxRangePosX, blockData.MaxRangePosX);
                float randomPosZ = Random.Range(-blockData.MaxRangePosZ, blockData.MaxRangePosZ);

                Vector3 newPos = new Vector3(randomPosX, 0, randomPosZ) + block.Centre.position;

                Debug.Log("Bomb spawned at " + newPos);

                var newSlime = _bombFactory.Get(newPos);
            }
        }
    }
}
