using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class TankComponent : IComponentData
{
    public const int STATUS_MOVE_TO_BOARD = 0;
    public const int STATUS_WAIT_FOR_MARCH = 1;
    public const int STATUS_SEARCH_FOR_ENEMY = 2;
    public const int STATUS_MOVE_CHASE_ENEMY = 3;
    public const int STATUS_SHOOT_AT_ENEMY = 4;
    public const int STATUS_PARTICLE_FLY = 5;
    public const int STATUS_DEAD = 99;
    
    
    
    public int TankID;
    public int EnemyID;
    public long ParticleFlyTimeStarded;
    public long ParticleFlyTimeEnds;
    public bool PlayerFaction;
    public GameObject Tank;
    public GameObject Bullet;
    public GameObject Explosion;
    public int TargetX;
    public int TargetY;
    public float EnemyX;
    public float EnemyY;
    public int Status;
    public int RemainingLife;

}
 