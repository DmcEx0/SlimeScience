using System;
using UnityEngine;

namespace SlimeScience.Configs
{
    [Serializable]
    public class SlimeConfig : MobileObjectConfig
    {
        [SerializeField] private float _distanceFofFear;
        [SerializeField] private float _fearSpeed;

        [SerializeField] private SlimeBuildData _buildData;

        public float DistanceFofFear => _distanceFofFear;
        public float FearSpeed => _fearSpeed;
        public SlimeBuildData BuildData => _buildData;
    }
}