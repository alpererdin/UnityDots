using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class UIComponentAuthoring : MonoBehaviour
{
    public GameObject CanvasPrefab;


}

public class UIComponentBaker:Baker<UIComponentAuthoring>
{
    public override void Bake(UIComponentAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.None);
        AddComponentObject(entity,new UIComponent
        {
            CanvasPrefab = authoring.CanvasPrefab
        });
    }
}