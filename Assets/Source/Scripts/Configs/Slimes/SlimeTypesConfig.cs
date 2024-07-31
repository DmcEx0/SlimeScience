using System;
using System.Collections.Generic;
using UnityEngine;

namespace SlimeScience.Configs.Slimes
{
    [Serializable]
    public class SlimeTypesConfig
    {
        [SerializeField] private List<SlimeTypeValues> _slimeTypeValues;

        public IReadOnlyList<SlimeTypeValues> SlimeTypeValues  => _slimeTypeValues;
    }
}