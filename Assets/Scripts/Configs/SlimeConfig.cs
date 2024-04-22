using System;
using UnityEngine;

[Serializable]
public class SlimeConfig : MobileObjectConfig
{
    [SerializeField] private Slime _prefab;

    [SerializeField] private float _distanceFofFear;
    [SerializeField] private float _fearSpeed;

    public Slime Prefab => _prefab;

    public float DistanceFofFear => _distanceFofFear;
    public float FearSpeed => _fearSpeed;
}