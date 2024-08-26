using System;
using SlimeScience.Traps;
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
