using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct ConstructionComponent : IComponentData
{
    public int PlayerOilRigs;
    public int EnemyOilRigs;
    public int PlayerFactories;
    public int EnemyFactories;
}
