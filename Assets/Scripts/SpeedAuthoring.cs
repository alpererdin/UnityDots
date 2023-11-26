using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class SpeedAuthoring : MonoBehaviour
{
    public float Value;
}

public class SpeedBaker : Baker<SpeedAuthoring>
{
    public override void Bake(SpeedAuthoring authoring)
    {
        //transform özellikleri 
      //  TransformUsageFlags flags = new TransformUsageFlags();//static
        Entity entity = GetEntity(TransformUsageFlags.Dynamic);
         
        // Component ekleme 
        AddComponent(entity, new Speed
        {
            Value = authoring.Value
        });
    }
     
}
