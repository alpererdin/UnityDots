using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class TankComponent : IComponentData
{
    public const int STATUS_MOVE_TO_BOARD=0;
    public const int STATUS_WAIT_FOR_MARCH=1;
    public bool PlayerFaction;
    public GameObject Tank;
    public GameObject Bullet;
    public GameObject Explosion;
    
    public int TargetX;
    public int TargetY;
    public int Status;
    public int RemainingLife;

}
 