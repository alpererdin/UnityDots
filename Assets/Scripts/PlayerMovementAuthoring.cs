using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEditor.SceneManagement;
using UnityEngine;

public class PlayerMovementAuthoring : MonoBehaviour
{

    public Vector2 PlayerMovement;
    public bool PlayerFiring;
    
}

public class PlayerMovementBaker : Baker<PlayerMovementAuthoring>
{
    public override void Bake(PlayerMovementAuthoring authoring)
    {
        Entity entity = GetEntity(TransformUsageFlags.Dynamic);

        AddComponent(entity, new PlayerMoveInput()
        {
            
        });
    }
}