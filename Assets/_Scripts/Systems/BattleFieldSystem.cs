using System;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public partial class BattleFieldSystem : SystemBase
{
    int nextAvailablePlacePlayerX = 1;
    int nextAvailablePlacePlayerY = 0;
    int nextAvailablePlaceEnemyX = 1;
    int nextAvailablePlaceEnemyY = 0;
    int width = 0;
    int height = 0;
    float reloadTime;
    float bulletSpeed;
    float tankSpeed;
    float tankFireDistance;
    float battleFieldPosPlayerX = 0;
    float battleFieldPosPlayerY = 0;
    float battleFieldPosEnemyX = 0;
    float battleFieldPosEnemyY = 0;

    protected override void OnStartRunning()
    {
        Entities.WithoutBurst().ForEach(
            (ref BattleFieldComponent component) =>
            {
                width = component.Width;
                height = component.Height;

                battleFieldPosPlayerX = component.PlayerFieldX;
                battleFieldPosPlayerY = component.PlayerFieldY;
                battleFieldPosEnemyX = component.EnemyFieldX;
                battleFieldPosEnemyY = component.EnemyFieldY;
            }).Run();

        Entities.WithoutBurst()
            .ForEach(
            (ref SettingsComponent component) =>
            {
                tankFireDistance = component.TankFireDistance;
                bulletSpeed = component.BulletSpeed;
                reloadTime = component.TankReloadTime;
                tankSpeed = component.TankSpeed;
            }).Run();
    }

    protected override void OnUpdate()
    {
        Vector3 target = Vector3.zero;
        Vector3 forward = Vector3.zero;
        float deltaTime = World.Time.DeltaTime;

        List<int> enemyIDToDamage = new List<int>();
        Entities.
        WithoutBurst().
        WithNone<TankEntityTag>().
        ForEach(
            (in TankComponent tank) =>
            {
                switch(tank.Status)
                {
                    case TankComponent.STATUS_MOVE_TO_BOARD:
                        // target
                        if(tank.TargetX == -1 || tank.TargetY == -1)
                        {
                            if (tank.PlayerFaction)
                            {
                                // player
                                tank.TargetX = nextAvailablePlacePlayerX;
                                tank.TargetY = nextAvailablePlacePlayerY;

                                if(++nextAvailablePlacePlayerX > width)
                                {
                                    nextAvailablePlacePlayerX = 1;
                                    nextAvailablePlacePlayerY += 1;
                                }
                            }
                            else
                            {
                                // enemy
                                tank.TargetX = nextAvailablePlaceEnemyX;
                                tank.TargetY = nextAvailablePlaceEnemyY;

                                if (++nextAvailablePlaceEnemyX > width)
                                {
                                    nextAvailablePlaceEnemyX = 1;
                                    nextAvailablePlaceEnemyY += 1;
                                }
                            }
                        }

                        // move
                        if (tank.PlayerFaction)
                        {
                            target = new Vector3(battleFieldPosPlayerX - 5.5f + tank.TargetX, tank.Tank.transform.position.y, battleFieldPosPlayerY - 5f + tank.TargetY * 1.25f);
                        }
                        else
                        {
                            target = new Vector3(battleFieldPosEnemyX - 5.5f + tank.TargetX, tank.Tank.transform.position.y, battleFieldPosEnemyY + 5f - tank.TargetY * 1.25f);
                        }

                        forward = target - tank.Tank.transform.position;
                        tank.Tank.transform.LookAt(target);
                        tank.Tank.transform.position += forward.normalized * tankSpeed * deltaTime;

                        if (Vector3.Distance(tank.Tank.transform.position, target) < 0.05f)
                        {
                            tank.Tank.transform.rotation = Quaternion.identity;
                            if (tank.PlayerFaction)
                            {
                                tank.Tank.transform.Rotate(Vector3.up * 180);
                            }

                            tank.Status = TankComponent.STATUS_WAIT_FOR_MARCH;
                        }

                        break;
                    case TankComponent.STATUS_SEARCH_FOR_ENEMY:
                        SearchForAnEnemy(tank, tank.Tank.transform);
                        break;
                    case TankComponent.STATUS_MOVE_CHASE_ENEMY:
                        ChaseEnemy(tank, tank.Tank.transform, deltaTime);
                        break;
                    case TankComponent.STATUS_SHOOT_AT_ENEMY:
                        target = new Vector3(tank.EnemyX, tank.Tank.transform.position.y, tank.EnemyY);
                        tank.Tank.transform.LookAt(target);
                        tank.Tank.transform.Find("ShootCloud").GetComponent<ParticleSystem>().Play();

                        // bullet
                        tank.Bullet = GameObject.Instantiate(tank.Tank.transform.Find("Shell").gameObject);
                        tank.Bullet.transform.position = tank.Tank.transform.position;
                        tank.Bullet.transform.rotation = tank.Tank.transform.rotation;

                        //calculate time
                        tank.ParticleFlyTimeStarded = DateTime.Now.Ticks;
                        float distance = Vector3.Distance(tank.Bullet.transform.position, target);
                        tank.ParticleFlyTimeEnds = tank.ParticleFlyTimeStarded + (long)(10000000 * (distance / bulletSpeed));
                        tank.Status = TankComponent.STATUS_PARTICLE_FLY;
                        break;
                    case TankComponent.STATUS_PARTICLE_FLY:
                        tank.Bullet.transform.position += tank.Bullet.transform.forward.normalized * bulletSpeed * deltaTime;

                        if (DateTime.Now.Ticks > tank.ParticleFlyTimeEnds)
                        {
                            tank.Bullet.SetActive(false);
                            enemyIDToDamage.Add(tank.EnemyID);
                            //hit 
                           
                        }

                        if (DateTime.Now.Ticks > tank.ParticleFlyTimeEnds + (long)(10000000 * reloadTime))
                        {
                            tank.Status = TankComponent.STATUS_SEARCH_FOR_ENEMY;
                        }

                        break;
                    case TankComponent.STATUS_WAIT_FOR_MARCH:

                        break;
                    
                     
                }
            }).Run();
        //--power
        int deadFromPlayer = 0;
        int deadFromEnemy = 0;

        Entities.
            WithoutBurst().
            WithStructuralChanges().
            WithNone<TankEntityTag>().
            ForEach(
                (Entity entity, in TankComponent tank) =>
                {
                    if (tank.Status == TankComponent.STATUS_DEAD)
                    { 
                        return;
                    }

                    for (int e = 0; e < enemyIDToDamage.Count; e++)
                    {
                        if (enemyIDToDamage[e] == tank.TankID)
                        {
                            tank.RemainingLife -= 1;
                            if (tank.RemainingLife <= 0)
                            {
                                tank.Status = TankComponent.STATUS_DEAD;
                                if(tank.PlayerFaction)
                                {
                                    deadFromPlayer += 1;
                                } else
                                {
                                    deadFromEnemy += 1;
                                }

                                EntityManager.AddComponentData(entity, new TankDeadTag { });

                                var explosion = GameObject.Instantiate(tank.Explosion, tank.Tank.transform.position, tank.Tank.transform.rotation);
                                explosion.SetActive(true);
                                explosion.GetComponent<ParticleSystem>().Play();

                                tank.Tank.SetActive(false);
                            }
                        }
                    }
                }).Run();

        // deadTanks
        Entities.ForEach(
            (ref BattleFieldComponent battleFieldComponent) =>
            {
                battleFieldComponent.NumAITanks -= deadFromEnemy;
                battleFieldComponent.NumPlayerTanks -= deadFromPlayer;
            }).Run();
        
     


    }
    private void ChaseEnemy(TankComponent tank, Transform transform, float deltaTime)
    {
        bool foundTarget = false;

        // check
        Entities.
            WithoutBurst().
            WithNone<TankEntityTag, TankDeadTag>().
            ForEach((ref LocalTransform targetTransform, in TankComponent targetTank) =>
            {
                if (foundTarget)
                {
                    return;
                }

                if (tank.PlayerFaction != targetTank.PlayerFaction)
                {
                    // fire check
                    float distance = Vector3.Distance(transform.position, targetTank.Tank.transform.position);
                    if (distance < tankFireDistance)
                    {
                        foundTarget = true;
                        tank.Status = TankComponent.STATUS_SHOOT_AT_ENEMY;
                        tank.EnemyX = targetTank.Tank.transform.position.x;
                        tank.EnemyY = targetTank.Tank.transform.position.z;
                        tank.EnemyID = targetTank.TankID;
                    }
                }
            }).Run();

        // new target
        if (!foundTarget)
        {
            Vector3 target = new Vector3(tank.EnemyX, tank.Tank.transform.position.y, tank.EnemyY);
            Vector3 forward = target - tank.Tank.transform.position;
            tank.Tank.transform.LookAt(target);
            tank.Tank.transform.position += forward.normalized * tankSpeed * deltaTime;

            if (Vector3.Distance(tank.Tank.transform.position, target) < 0.05f)
            {
                tank.Tank.transform.rotation = Quaternion.identity;
                if (tank.PlayerFaction)
                {
                    tank.Tank.transform.Rotate(Vector3.up * 180);
                }

                tank.Status = TankComponent.STATUS_SEARCH_FOR_ENEMY;
            }
        }
    }

    private void SearchForAnEnemy(TankComponent tank, Transform transform)
    {
       
        bool foundTarget = false;
        Entities.
            WithoutBurst().
            WithNone<TankEntityTag,TankDeadTag>().
            ForEach((ref LocalTransform targetTransform, in TankComponent targetTank) =>
        {
            if (foundTarget)
            {
                return;
            }

            if (tank.PlayerFaction != targetTank.PlayerFaction)
            {
                float distance = Vector3.Distance(transform.position,targetTransform.Position);
                if (distance < tankFireDistance)
                {
                    foundTarget = true;
                    tank.Status = TankComponent.STATUS_SHOOT_AT_ENEMY;
                    tank.EnemyX = targetTank.Tank.transform.position.x;
                    tank.EnemyY = targetTank.Tank.transform.position.z;
                    // enemy


                }
            }
            
        }).Run();
        if (!foundTarget)
        {
            float closesDist = float.MaxValue;
            float dist = 0;
            Entities.
                WithoutBurst().
                WithNone<TankEntityTag,TankDeadTag>().
                ForEach((ref LocalTransform targetTransform, in TankComponent targetTank) =>
                {
                    if (foundTarget)
                    {
                        return;
                    }
                    dist = Vector3.Distance(transform.position, targetTransform.Position);
                    if (dist<closesDist)
                    {
                        foundTarget = true;
                        closesDist = dist;
                        tank.EnemyX = targetTank.Tank.transform.position.x;
                        tank.EnemyY = targetTank.Tank.transform.position.z;
              
                    }

                }).Run();

        }
        else
        {
            return;
        }

        if (foundTarget)
        {
            tank.Status = TankComponent.STATUS_MOVE_CHASE_ENEMY;
          
            //pos


        }
    }
}








