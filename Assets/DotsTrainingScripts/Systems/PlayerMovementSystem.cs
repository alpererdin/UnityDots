using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial class PlayerMovementSystem :SystemBase
{
    protected override void OnUpdate()
    {
        float deltaTime = World.Time.DeltaTime *10f;
 
        
        Entities.ForEach(
        (ref LocalTransform transform, in PlayerMoveInput input)=>
        {
            transform.Position.xz += input.PlayerMove * deltaTime;
            if (input.PlayerMove.x !=0 || input.PlayerMove.y !=0 )
            {
                transform.Rotation = quaternion.LookRotation(new float3(input.PlayerMove.x, 0, input.PlayerMove.y),math.up());
            }

            if (input.Firing)
            {
                Debug.Log("fire");
            }
            
        }).Run();
       
    }
}
