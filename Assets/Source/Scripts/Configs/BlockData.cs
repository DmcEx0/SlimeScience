using System;
using UnityEngine;

namespace SlimeScience.Configs
{
    [Serializable]
    public class BlockData
    {
        [SerializeField] private GameObject _block;
        [SerializeField] private int _slimeAmount;
        [SerializeField] private int _neededAmountToOpen;
        [SerializeField] private float _maxRangePosX;
        [SerializeField] private float _maxRangePosZ;

        public GameObject Block => _block;
        public int SlimeAmount => _slimeAmount;
        public int NeededAmountToOpen => _neededAmountToOpen;
        public float MaxRangePosX => _maxRangePosX;
        public float MaxRangePosZ => _maxRangePosZ;
    }
}