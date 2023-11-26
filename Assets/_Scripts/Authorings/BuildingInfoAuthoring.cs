using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class BuildingInfoAuthoring : MonoBehaviour
{


    public bool Build;
    public int Index;
    public string BuildingType;
    public GameObject Prefab;
   
}

public class BuildInfoBaker : Baker<BuildingInfoAuthoring>
{
    public override void Bake(BuildingInfoAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.Dynamic);
        AddComponentObject(entity,new BuildingInfoComponent
        {
            Built = authoring.Build,
            Index = authoring.Index,
            BuildingType = authoring.BuildingType,
            Prefab = authoring.Prefab,
            IsProducing = false,
            TimeToFinishProduction = 0
        });
    }
}