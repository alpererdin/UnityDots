using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class AIComponentAuthoring : MonoBehaviour
{
     
    public float AIBuildTimeFactor;
    public float AIBuildSpendingFactor;
    public float AITankLifeFactor;
}

public class AIComponentBaker : Baker<AIComponentAuthoring>
{
    public override void Bake(AIComponentAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.None);

        AddComponent(entity, new AIComponent
        {
            NumFactories = 0,
            NumOilRigs = 1,
            AIBuildSpendingFactor = authoring.AIBuildSpendingFactor,
            AIBuildTimeFactor = authoring.AIBuildTimeFactor,
            AITankLifeFactor = authoring.AITankLifeFactor
        });
    }
}