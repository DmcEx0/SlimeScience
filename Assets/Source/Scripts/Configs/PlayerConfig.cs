using System;
using SlimeScience.Characters;
using UnityEngine;

namespace SlimeScience.Configs
{
    [Serializable]
    public class PlayerConfig : MobileObjectConfig
    {
        [SerializeField] private Player _prefab;

        [SerializeField] private float _rangeVacuum;

        public Player Prefab => _prefab;

        public float RangeVacuum => _rangeVacuum;
    }
}