using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class TankComponentAuthoring : MonoBehaviour
{
    public GameObject Explosion;
    
}

public class TankComponentBaker : Baker<TankComponentAuthoring>
{
    public override void Bake(TankComponentAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.Dynamic);
        AddComponentObject(entity, new TankComponent
        {
            Explosion = authoring.Explosion
            
        });
        AddComponent(entity, new TankEntityTag
        {
            
        });
        
    }
}
