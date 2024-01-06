using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Physics;
using Unity.Physics.Systems;
using UnityEngine;

[UpdateInGroup(typeof(PhysicsSystemGroup))]
[UpdateAfter(typeof(PhysicsInitializeGroup))]
public partial struct CollisionProcessorSystem : ISystem
{   
    /*[BurstCompile]
    public partial struct CountNumCollisionEvents: ICollisionEventsJob
    {
        public NativeReference<int> NumCollisions;
        
        
        public void Execute(CollisionEvent collisionEvent)
        {
            NumCollisions.Value += 1;
            
        }
    }
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        NativeReference<int> numCollisions = new NativeReference<int>(0, Allocator.TempJob);

        var job = new CountNumCollisionEvents
        {
            NumCollisions = numCollisions
        };
        job.Schedule<CountNumCollisionEvents>(
            SystemAPI.GetSingleton<SimulationSingleton>(),
                state.Dependency
            ).Complete();
        
        Debug.Log("callided "+ numCollisions.Value);
    }
    */
    
}
