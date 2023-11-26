using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class ConstructionAuthoring : MonoBehaviour
{
    public int PlayerOilRigs=1;
    public int EnemyOilRigs=1;
    public int PlayerFactories=0;
    public int EnemyFactories=0;
}

public class ConstructionBaker : Baker<ConstructionAuthoring>
{
    public override void Bake(ConstructionAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.None);
        
        AddComponent(entity,new ConstructionComponent
        {
            PlayerFactories = authoring.PlayerFactories,
            EnemyFactories = authoring.EnemyFactories,
            PlayerOilRigs = authoring.PlayerOilRigs,
            EnemyOilRigs = authoring.EnemyOilRigs
        });
    }
}
