using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using System;

public class CentralRandomAuthoring : MonoBehaviour
{
   
    
}

public class CentralRandomBaker : Baker<CentralRandomAuthoring>
{
    public override void Bake(CentralRandomAuthoring authoring)
    {
        Entity entity = GetEntity(TransformUsageFlags.None);
        
        AddComponent(entity,new CentralRandomComponent()
        {
            Rand = new Unity.Mathematics.Random((uint)DateTime.Now.Ticks)
        });
    }
}
