using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class SpawnParametersAuthoring : MonoBehaviour
{
    public GameObject EntityPrefab;
    public int GridWidth;
    public int GridHeight;
    public float Spacing;
    


}

/*public partial class SpawnParamsBaker : Baker<SpawnParametersAuthoring>
{
    public override void Bake(SpawnParametersAuthoring authoring)
    {
        Entity entity = GetEntity(TransformUsageFlags.Dynamic);
        
        
        AddComponent(entity, new SpawnParameters()
        {
            EntityPrefab = GetEntity(authoring.EntityPrefab,TransformUsageFlags.Dynamic),
            GridWidth = authoring.GridWidth,
            GridHeight = authoring.GridHeight,
            Spacing = authoring.Spacing
        });
    }
}*/