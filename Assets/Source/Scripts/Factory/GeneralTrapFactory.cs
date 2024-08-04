using System.Collections.Generic;
using SlimeScience.Configs;
using SlimeScience.Util;
using UnityEngine;

namespace SlimeScience.Factory
{
    [CreateAssetMenu(fileName = "GeneralTrapFactory", menuName = "Factories/General/Trap")]
    public class GeneralTrapFactory : TrapFactory
    {
        [SerializeField] private List<TrapConfig> _configs;

        private List<TrapConfig> _currentConfigs = new List<TrapConfig>();
        private int _currentIndex;

        protected override TrapConfig GetNextConfig()
        {
            if (_currentIndex >= _currentConfigs.Count - 1)
            {
                _currentIndex = 0;
                _currentConfigs.Clear();
                _currentConfigs.AddRange(_configs);
                
                Shuffler.Shuffle(ref _currentConfigs);
            }
            
            return _currentConfigs[_currentIndex++];
        }
    }
}