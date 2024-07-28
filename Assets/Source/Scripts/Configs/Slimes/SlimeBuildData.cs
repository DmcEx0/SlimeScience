using SlimeScience.Characters;
using System;
using UnityEngine;

namespace SlimeScience.Configs.Slimes
{
    [Serializable]
    public class SlimeBuildData
    {
        [SerializeField] private Slime[] _prefabs;
        [SerializeField] private GameObject[] _hats;
        [SerializeField] private Material[] _bodyMaterials;
        [SerializeField] private Material[] _faceMaterials;

        public Slime GetRandomPrefab => _prefabs[UnityEngine.Random.Range(0, _prefabs.Length)];
        public GameObject GetRandomHat => _hats[UnityEngine.Random.Range(0, _hats.Length)];
        public Material GetRandomBodyMaterial => _bodyMaterials[UnityEngine.Random.Range(0, _bodyMaterials.Length)];
        public Material GetRandomFaceMaterial => _faceMaterials[UnityEngine.Random.Range(0, _faceMaterials.Length)];
    }
}
