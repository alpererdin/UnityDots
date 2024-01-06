using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
 

/*public partial class SpawningSystem :  SystemBase
{
    protected override void OnStartRunning()
    {
        SpawnParameters parameters= SystemAPI.GetSingleton<SpawnParameters>();
        LocalTransform localTransform;
        
        int total = 0;
        
        for (int i = 0; i < parameters.GridWidth; i++)
        {
            for (int j = 0; j < parameters.GridHeight; j++)
            {
               
                 var newEntity = EntityManager.Instantiate(parameters.EntityPrefab);
                 localTransform = new LocalTransform()
                 {
                     Position = new float3(i * parameters.Spacing, 0, j * parameters.Spacing),
                     Rotation = quaternion.identity,
                     Scale = 1f
                 };
                 EntityManager.SetComponentData(newEntity,localTransform);
                 
                 total += 1;
                 if (total %3 ==0)
                 {
                     EntityManager.AddComponent<CapsuleTag>(newEntity);
                 }
            }
  
        }
        //DoTheJob();
    }

    protected override void OnUpdate()
    {
         
    }

   
}*/
 