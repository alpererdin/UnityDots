using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;
using Unity.VisualScripting;
using UnityEngine;

public partial class BenchmarkSystem : SystemBase
{
    protected override void OnUpdate()
    {
        float deltaTime = World.Time.DeltaTime;
        Unity.Mathematics.Random random = new Unity.Mathematics.Random((uint)DateTime.Now.Ticks);
        var ecb = new EntityCommandBuffer(Unity.Collections.Allocator.TempJob);

        Entities.ForEach(
            (ref BenchMarkComponent component, in LocalTransform throwingLocation) =>
            {
                if (component.CurrentBalls < component.MaxBalls)
                {
                    if (component.CurrentThrowPeriod < component.CurrentThrowTime)
                    {
                        //throw
                        component.CurrentThrowPeriod =
                            random.NextFloat(component.MinThrowPeriod, component.MaxThrowPeriod);
                        component.CurrentThrowTime = 0;
                        component.CurrentBalls += 1;

                        Entity newEntity;
                        switch (random.NextInt(0,5))
                        {
                            case 0:
                                newEntity = ecb.Instantiate(component.Entity1);
                              break;
                            case 1:
                                newEntity = ecb.Instantiate(component.Entity2);
                                break;
                            case 2:
                                newEntity = ecb.Instantiate(component.Entity3);
                                break;
                            case 3:
                                newEntity = ecb.Instantiate(component.Entity4);
                                break;
                            case 4:
                                newEntity = ecb.Instantiate(component.Entity5);
                                break;
                            default:
                                newEntity = ecb.Instantiate(component.Entity1);
                                break;
                            
                        }

                        var transform = new LocalTransform
                        {
                            Position = throwingLocation.Position,
                            Rotation = quaternion.identity,
                            Scale = 1f
                        };
                        var velocity = new PhysicsVelocity
                        {
                            Linear = component.ThrowingAngle*random.NextFloat(component.MinThrowSpeed,component.MaxThrowSpeed),
                            Angular = float3.zero
                        };
                        ecb.AddComponent(newEntity,transform);
                        ecb.AddComponent(newEntity,velocity);
                    }
                    else
                    {
                        component.CurrentThrowTime += deltaTime;
                    }
                

                }
            }
        ).Schedule();
        Dependency.Complete();
        ecb.Playback(EntityManager);
        ecb.Dispose();
    }
}
