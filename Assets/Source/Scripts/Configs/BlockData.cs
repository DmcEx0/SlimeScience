using System;
using SlimeScience.Characters;
using UnityEngine;

namespace SlimeScience.Configs
{
    [Serializable]
    public class BlockData
    {
        [SerializeField] private int _bombAmount;
        [SerializeField] private int _slimeAmount;
        [SerializeField] private int _neededAmountToOpen;
        [SerializeField] private float _maxRangePosX;
        [SerializeField] private float _maxRangePosZ;
        [SerializeField] private SlimeType _slimesType;

        public int BombAmount => _bombAmount; 
        public int SlimeAmount => _slimeAmount;
        public int NeededAmountToOpen => _neededAmountToOpen;
        public float MaxRangePosX => _maxRangePosX;
        public float MaxRangePosZ => _maxRangePosZ;
        public SlimeType SlimesType => _slimesType;
    }
}