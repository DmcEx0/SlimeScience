using System.Collections.Generic;
using UnityEngine;

namespace SlimeScience.Configs.Slimes
{
    [CreateAssetMenu(fileName = "SlimeTypesConfig", menuName = "Configs/Slimes/SlimeTypesConfig")]
    public class SlimeTypesConfig : ScriptableObject
    {
        [SerializeField] private List<SlimeTypeValues> _slimeTypeValues;

        public List<SlimeTypeValues> SlimeTypeValues => _slimeTypeValues;
    }
}
