using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

/*public partial class BufferSystem : SystemBase
{
    Entity bufferEntity;
    protected override void OnCreate()
    {
        bufferEntity = EntityManager.CreateEntity();

        var buf = EntityManager.AddBuffer<SumBuffer>(bufferEntity);
        buf.Add(new SumBuffer{Value = 1});
        buf.Add(new SumBuffer{Value = 4});
        buf.Add(new SumBuffer{Value = 6});
        buf.Add(new SumBuffer{Value = 9});

    }

    protected override void OnUpdate()
    {
        DynamicBuffer<SumBuffer> buffer = EntityManager.GetBuffer<SumBuffer>(bufferEntity);
        int sum = 0;
        for (int i = 0; i < buffer.Length; i++)
        {
            sum += buffer[i].Value;
        }
        Debug.Log(sum);
        
    }
}
*/