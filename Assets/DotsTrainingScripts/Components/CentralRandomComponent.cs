using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct CentralRandomComponent : IComponentData
{
    public Unity.Mathematics.Random Rand;
}
