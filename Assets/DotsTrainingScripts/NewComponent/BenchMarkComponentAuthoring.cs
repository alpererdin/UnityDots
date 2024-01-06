using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class BenchMarkComponentAuthoring : MonoBehaviour
{
   public GameObject[] Prefabs;
   public float MinThrowPeriod = 1.0f;
   public float MaxThrowPeriod = 3.0f;
   public float MinThrowSpeed = 1.34f;
   public float MaxThrowSpeed = 1.67f;

   public int MaxBalls = 650;
}

public class BenchMarkBaker : Baker<BenchMarkComponentAuthoring>
{
   public override void Bake(BenchMarkComponentAuthoring authoring)
   {
      var entity = GetEntity(TransformUsageFlags.Dynamic);
      AddComponent(entity,new BenchMarkComponent
      {
         Entity1 = GetEntity(authoring.Prefabs[0],TransformUsageFlags.Dynamic),
         Entity2 = GetEntity(authoring.Prefabs[1],TransformUsageFlags.Dynamic),
         Entity3 = GetEntity(authoring.Prefabs[2],TransformUsageFlags.Dynamic),
         Entity4 = GetEntity(authoring.Prefabs[3],TransformUsageFlags.Dynamic),
         Entity5 = GetEntity(authoring.Prefabs[4],TransformUsageFlags.Dynamic),
         CurrentThrowPeriod = 0,
         CurrentThrowTime = 0,
         CurrentBalls = 0,
         MaxBalls = authoring.MaxBalls,
         MaxThrowPeriod = authoring.MaxThrowPeriod,
         MinThrowPeriod = authoring.MinThrowPeriod,
         MinThrowSpeed = authoring.MinThrowSpeed,
         MaxThrowSpeed = authoring.MaxThrowSpeed,
         ThrowingAngle = -authoring.transform.forward
         
      });
   }
}
