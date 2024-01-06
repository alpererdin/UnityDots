using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

/*public partial class TransformSystem : SystemBase
{
    

    protected override void OnUpdate()
    {
        float deltaTime = World.Time.DeltaTime;
        Entities.
            ForEach(
               (ref LocalTransform localTrans, in TransformComponent trans) =>
               {
                   localTrans =
                       localTrans.Rotate(quaternion.Euler(new float3(trans.Rotation.x, trans.Rotation.y, trans.Rotation.z)*deltaTime));
               }
              ).Run();
    }
}
*/