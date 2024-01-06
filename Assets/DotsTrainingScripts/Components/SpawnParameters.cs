using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct SpawnParameters : IComponentData
{

    public Entity EntityPrefab;
    public int GridWidth;
    public int GridHeight;
    public float Spacing;
}
