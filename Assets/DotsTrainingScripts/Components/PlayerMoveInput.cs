using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public struct PlayerMoveInput : IComponentData
{
   public float2 PlayerMove;
   public bool Firing;
   

}
