using System;
using SlimeScience.Characters;
using UnityEngine;
using UnityEngine.Serialization;

namespace SlimeScience.Configs
{
    [Serializable]
    public class SlimeTypeValues
    {
        [FormerlySerializedAs("_slimeType")] [SerializeField] private SlimeType _type;
        [SerializeField] private int _weight;
        [SerializeField] private Vector3 _scale;
        public SlimeType Type => _type;
        public int Weight => _weight;
        public Vector3 Scale => _scale;
    }
}
