using System;
using SlimeScience.Configs;
using UnityEngine;

namespace SlimeScience
{
    [Serializable]
    public class VacuumingSupportConfig : MobileObjectConfig
    {
        [SerializeField] private VacuumingSupport _prefab;
        [SerializeField] private float _distanceFofIncreaseSpeed;
        [SerializeField] private float _detectedSpeed;
        [SerializeField] private int _inventoryCapacity;

        public VacuumingSupport Prefab => _prefab;
        public float DistanceFofIncreaseSpeed => _distanceFofIncreaseSpeed;
        public float DetectedSpeed => _detectedSpeed;
        public int InventoryCapacity => _inventoryCapacity;
    }
}
