using System;
using UnityEngine;

namespace SlimeScience.Upgrades
{
    [Serializable]
    public class UpgradeLevel
    {
        [SerializeField] private int _cost;

        [SerializeField] private int _value;

        public int Cost => _cost;

        public int Value => _value;
    }
}
