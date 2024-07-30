using System;
using SlimeScience.Characters;
using UnityEngine;

namespace SlimeScience.Configs.Slimes
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
        public int Weight { get; private set; }
       public SlimeType Type { get; private set; }
        
        public void SetType(SlimeType type)
        {
            Type = type;
        }
        
        public void SetWeight(int weight)
        {
            Weight = weight;
        }
    }
}