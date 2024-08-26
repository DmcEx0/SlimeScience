using System.Collections.Generic;
using SlimeScience.Blocks;
using SlimeScience.Configs;
using SlimeScience.Factory;
using SlimeScience.Traps;
using SlimeScience.Util;
using UnityEngine;

namespace SlimeScience.Spawners
{
    public class TrapSpawner
    {
        private readonly TrapFactory _trapFactory;
        private Dictionary<Block, List<Transform>> _blocks;

        public TrapSpawner(TrapFactory trapFactory)
        {
            _trapFactory = trapFactory;
            _blocks = new Dictionary<Block, List<Transform>>();
        }

        public void Spawn(BlockData blockData, Block block)
        {
            List<Transform> points = new List<Transform>();

            IdentifySpawnPoint(block, ref points);

            for (int i = 0; i < blockData.TrapAmount; i++)
            {
                ConfigureTrap(block, points[i]);
            }
        }

        private void Respawn(Trap trap, Block block, Transform previousPoint)
        {
            trap.Activated -= Respawn;
            List<Transform> points = _blocks[block];

            Shuffler.Shuffle(ref points);

            foreach (var point in points)
            {
                if (point.gameObject.activeInHierarchy)
                {
                    ConfigureTrap(block, point);

                    break;
                }
            }

            previousPoint.gameObject.SetActive(true);
        }

        private void ConfigureTrap(Block block, Transform point)
        {
            var trap = _trapFactory.Get(point.position);

            trap.SetBlock(block);
            trap.SetPoint(point);

            point.gameObject.SetActive(false);
            trap.Activated += Respawn;
        }

        private void IdentifySpawnPoint(Block block, ref List<Transform> points)
        {
            for (int i = 0; i < block.TrapsParent.childCount; i++)
            {
                points.Add(block.TrapsParent.GetChild(i));
            }

            Shuffler.Shuffle(ref points);

            _blocks.Add(block, points);
        }
    }
}