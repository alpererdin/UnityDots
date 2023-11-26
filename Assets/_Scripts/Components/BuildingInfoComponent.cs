using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

public class BuildingInfoComponent : IComponentData
{
    public bool Built;
    public int Index;
    public bool IsProducing;
    public long TimeToFinishProduction;
    public long TimeStartedToProduct;
    public FixedString32Bytes BuildingType;
    public GameObject Prefab;
    public GameObject SceneObject;

}
