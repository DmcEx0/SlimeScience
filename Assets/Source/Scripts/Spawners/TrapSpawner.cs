using System.Collections.Generic;
using SlimeScience.Blocks;
using SlimeScience.Configs;
using SlimeScience.Factory;
using SlimeScience.Util;
using UnityEngine;

namespace SlimeScience.Spawners
{
    public class TrapSpawner
    {
        private TrapFactory _trapFactory;
        private List<Transform> _points;

        public TrapSpawner(TrapFactory trapFactory)
        {
            _trapFactory = trapFactory;
            _points = new List<Transform>();
        }

        public void Spawn(BlockData blockData, Block block)
        {
            IdentifySpawnPoint(block.TrapsParent);

            for (int i = 0; i < blockData.TrapAmount; i++)
            {
                _trapFactory.Get(_points[i].position);
            }
        }

        private void IdentifySpawnPoint(Transform parent)
        {
            for (int i = 0; i < parent.childCount; i++)
            {
                _points.Add(parent.GetChild(i));
            }
            
            Shuffler.Shuffle(ref _points);
        }
    }
}