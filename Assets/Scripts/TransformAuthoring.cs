using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

/*public class TransformAuthoring : MonoBehaviour
{

    public Vector3 Rotation;
    
}

public class TransformBaker : Baker<TransformAuthoring>
{
    public override void Bake(TransformAuthoring authoring)
    {
        Entity entity = GetEntity(TransformUsageFlags.Dynamic);
        AddComponent(entity,new TransformComponent()
        {
            Rotation = authoring.Rotation
        });
        AddComponent(entity,new LocalTransform()
        {
            Position = new float3(authoring.transform.localPosition.x,authoring.transform.localPosition.y,authoring.transform.localPosition.z),
            Rotation = quaternion.Euler(authoring.transform.localPosition.x,authoring.transform.localPosition.y,authoring.transform.localPosition.z),
            Scale = 1
        });
    }
}*/
