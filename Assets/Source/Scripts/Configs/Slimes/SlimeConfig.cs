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

        [SerializeField] private int _normalWeight;
        [SerializeField] private int _bigWeight;
        [SerializeField] private int _teleportWeight;

        public float DistanceFofFear => _distanceFofFear;
        public float FearSpeed => _fearSpeed;
        public SlimeBuildData BuildData => _buildData;
        public int NormalWeight => _normalWeight;
        public int BigWeight => _bigWeight;
        public int TeleportWeight => _teleportWeight;
       public SlimeType Type { get; private set; }
        
        public void SetType(SlimeType type)
        {
            Type = type;
        }
    }
}