using System.Collections.Generic;
using UnityEngine;

public abstract class MobileObjectConfig
{
    [SerializeField] private float _baseSpeed;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _acceleration;

    public float BaseSpeed => _baseSpeed;
    public float AngularSpeed => _rotateSpeed;
    public float Acceleration => _acceleration;
}