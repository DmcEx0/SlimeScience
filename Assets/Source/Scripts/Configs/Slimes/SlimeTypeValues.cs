using System;
using SlimeScience.Characters;
using UnityEngine;

namespace SlimeScience.Configs
{
    [Serializable]
    public class SlimeTypeValues
    {
        [SerializeField] private SlimeType _slimeType;
        [SerializeField] private int _weight;

        public SlimeType SlimeType => _slimeType;

        public int Weight => _weight;
    }
}
