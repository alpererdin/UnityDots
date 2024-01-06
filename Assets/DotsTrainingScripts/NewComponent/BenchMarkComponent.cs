using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public struct BenchMarkComponent : IComponentData
{
    public Entity Entity1;
    public Entity Entity2;
    public Entity Entity3;
    public Entity Entity4;
    public Entity Entity5;

    public float3 ThrowingAngle;
    public float MinThrowPeriod;
    public float MaxThrowPeriod;
    public float MinThrowSpeed;
    public float MaxThrowSpeed;

    public int MaxBalls;
    public int CurrentBalls;

    public float CurrentThrowPeriod;
    public float CurrentThrowTime;
}
