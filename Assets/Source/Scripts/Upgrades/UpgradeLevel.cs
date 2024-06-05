using System;
using UnityEngine;

namespace SlimeScience.Upgrades
{
    [Serializable]
    public class UpgradeLevel
    {
        [SerializeField] private int _cost;

        [SerializeField] private float _value;

        public int Cost => _cost;

        public float Value => _value;
    }
}
