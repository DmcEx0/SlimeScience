using System;
using SlimeScience.Blocks;
using SlimeScience.Configs;
using UnityEngine;

namespace SlimeScience.Traps
{
    [RequireComponent(typeof(Collider))]
    public class Trap : MonoBehaviour
    {
        private Block _block;
        private Transform _point;

        public event Action<Trap, Block, Transform> Activated;

        public TrapConfig Config { get; private set; }

        public void Init(TrapConfig config)
        {
            Config = config;
        }

        public void SetBlock(Block block)
        {
            _block = block;
        }

        public void SetPoint(Transform point)
        {
            _point = point;
        }

        public void CallActivatedEvent()
        {
            Activated?.Invoke(this, _block, _point);
        }
    }
}
