using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct AIComponent : IComponentData
{
    public int NumFactories;
    public int NumOilRigs;
    public float AIBuildTimeFactor;
    public float AIBuildSpendingFactor;
    public float AITankLifeFactor;
    

}
