using SlimeScience.Traps;
using System;
using UnityEngine;

namespace SlimeScience.Configs
{
    [Serializable]
    public class BombConfig
    {
        [SerializeField] private Bomb _prefab;

        public Bomb Prefab => _prefab;
    }
}
