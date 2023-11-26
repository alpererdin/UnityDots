using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

/*public partial class MovementSystem : SystemBase
{
    private BeginSimulationEntityCommandBufferSystem entityCommandBufferSystem;
    
    protected override void OnCreate()
    {
        //ISystem:  burstcompile**
        // car systemHandle = state.WorldUnmanaged.GetSystem<EndSimulationEntityCommandBufferSystem>();
        //  state.WorldUnmanaged.GetUnsafeSystem<EndSimulationEntityCommandBufferSystem>(systemHandle);
        
        entityCommandBufferSystem = World.GetExistingSystemManaged<BeginSimulationEntityCommandBufferSystem>();
    }

    protected override void OnUpdate()
    {
        EntityCommandBuffer ecb = entityCommandBufferSystem.CreateCommandBuffer();
     //   /*foreach(var(transform,speed) in SystemAPI.Query<RefRW<LocalTransform>,RefRO<Speed>>())
             {
                  transform.ValueRW.Position += new Unity.Mathematics.float3(0, 0, speed.ValueRO.Value * deltaTime );

          //   }
      
      float deltaTime =  World.Time.DeltaTime;
      
        Entities.
         WithAll<CapsuleTag, LocalTransform, Speed>().
           ForEach(
                (Entity entity,ref LocalTransform transform, in Speed speed) => 
                {
                transform.Position += new Unity.Mathematics.float3(0, 0, speed.Value * deltaTime);
                    if (transform.Position.z> 20 )
                    {
                        ecb.DestroyEntity(entity);
                    } 
                }
            ).Run();
             
    }
}*/