using System;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.VisualScripting;
using UnityEngine;

public partial class AISystem : SystemBase
{
    bool buildingInProgress = false;
    protected override void OnUpdate()
    {
    
     int buildFactoryOnto = 0;
     int buildOilRigOnto = 0;
     bool buildATank = false;
     var buildingInProgress = this.buildingInProgress;
      if (!buildingInProgress)
        {
            Entities.
            ForEach(
                (ref AIComponent ai, ref IncomeComponent income, ref SettingsComponent settings) =>
                {
                    // inşaat yapmak
                    if (ai.NumOilRigs == 1 && ai.NumFactories == 0)
                    {
                        // bir fabrika inşa etmek hakkında düşünelim
                        if (income.IncomeEnemy >= settings.CostOfFactoryBuild * ai.AIBuildSpendingFactor)
                        {
                            UnityEngine.Debug.Log("AI decided to build a factory");
                            income.IncomeEnemy -= (long)(settings.CostOfFactoryBuild * ai.AIBuildSpendingFactor);
                            buildingInProgress = true;

                            buildFactoryOnto = ai.NumFactories + 1;
                        }
                    }
                    else if (ai.NumOilRigs <= 2 && ai.NumFactories == 1)
                    {
                        // bir petrol kuyusu inşa etmek hakkında düşünelim
                        if (income.IncomeEnemy >= settings.CostOfOilRigBuild * ai.AIBuildSpendingFactor)
                        {
                            UnityEngine.Debug.Log("AI decided to build a oil rig");
                            income.IncomeEnemy -= (long)(settings.CostOfOilRigBuild * ai.AIBuildSpendingFactor);
                            buildingInProgress = true;

                            buildOilRigOnto = ai.NumOilRigs + 1;
                        }
                    }
                    else if (ai.NumTanks < 5)
                    {
                        // 5 tane tank yapalım
                        if (income.IncomeEnemy >= settings.CostOfTankBuild * ai.AIBuildSpendingFactor)
                        {
                            UnityEngine.Debug.Log("AI decided to build a tank");
                            income.IncomeEnemy -= (long)(settings.CostOfTankBuild * ai.AIBuildSpendingFactor);
                            buildingInProgress = true;

                            buildATank = true;
                        }
                    }
                    else if (ai.NumOilRigs <= 3 && ai.NumFactories == 1)
                    {
                        // bir fabrika daha yapalım
                        UnityEngine.Debug.Log("AI decided to build a factory");
                        income.IncomeEnemy -= (long)(settings.CostOfFactoryBuild * ai.AIBuildSpendingFactor);
                        buildingInProgress = true;

                        buildFactoryOnto = ai.NumFactories + 1;
                    }
                    else if (ai.NumTanks < 10)
                    {
                        // 5 tane tank yapalım
                        if (income.IncomeEnemy >= settings.CostOfTankBuild * ai.AIBuildSpendingFactor)
                        {
                            UnityEngine.Debug.Log("AI decided to build a tank");
                            income.IncomeEnemy -= (long)(settings.CostOfTankBuild * ai.AIBuildSpendingFactor);
                            buildingInProgress = true;

                            buildATank = true;
                        }
                    }
                    else if (ai.NumOilRigs <= 3 && ai.NumFactories == 2)
                    {
                        // bir fabrika daha yapalım
                        UnityEngine.Debug.Log("AI decided to build a factory");
                        income.IncomeEnemy -= (long)(settings.CostOfFactoryBuild * ai.AIBuildSpendingFactor);
                        buildingInProgress = true;

                        buildFactoryOnto = ai.NumFactories + 1;
                    }
                    else if (ai.NumTanks < 100)
                    {
                        // 100e kadar tank yapalım
                        if (income.IncomeEnemy >= settings.CostOfTankBuild * ai.AIBuildSpendingFactor)
                        {
                            UnityEngine.Debug.Log("AI decided to build a tank");
                            income.IncomeEnemy -= (long)(settings.CostOfTankBuild * ai.AIBuildSpendingFactor);
                            buildingInProgress = true;

                            buildATank = true;
                        }
                    }

                    // tank inşa etmek
                }).Run();

            
                if (buildFactoryOnto > 0)
                {
                    BuildFactory(buildFactoryOnto-1);
                }else if (buildOilRigOnto > 0)
                {
                    BuildOilRig(buildOilRigOnto-1);
                }
                if (buildATank)
                {
                    BuildTank();
                }
             
     }
       
        //
        bool finishedOilRig = false;
        bool finishedFactory = false;
        bool finishedTank = false;
        List<GameObject> finishedTanks = new List<GameObject>();
         Entities.
            WithoutBurst().
            ForEach(
            (ref LocalTransform transform, in BuildingInfoComponent info) =>
            {
                if (info.IsProducing && (info.BuildingType == "EnemyOilRig" || info.BuildingType == "EnemyFactory"))
                {
                    if (info.TimeToFinishProduction < DateTime.Now.Ticks)
                    {
                        // inşa bitmiş demektir
                        if (info.BuildingType == "EnemyOilRig")
                        {
                            // petrol kuyusu inşaatı bitmiş
                            UnityEngine.Debug.Log("AI has completed an oil rig");

                            info.SceneObject = GameObject.Instantiate(info.Prefab, transform.Position, quaternion.identity);
                            info.SceneObject.transform.localScale = new Vector3(0.67f, 0.67f, 0.34f);

                            finishedOilRig = true;
                        }
                        else if (info.BuildingType == "EnemyFactory")
                        {
                            if (!info.Built)
                            {
                                // fabrika inşaatı bitmiş
                                UnityEngine.Debug.Log("AI has completed a factory");

                                info.SceneObject = GameObject.Instantiate(info.Prefab, transform.Position, quaternion.identity);
                                info.SceneObject.transform.localScale = new Vector3(0.5f, 0.5f, -0.5f);
                                info.SceneObject.transform.Rotate(Vector3.up );//* -90f

                                finishedFactory = true;
                            }
                            else
                            {
                                // tank imalatı bitmiş
                                UnityEngine.Debug.Log("AI has completed a tank");

                                var tank = GameObject.Instantiate(info.TankPrefab);
                                tank.transform.position = new Vector3(transform.Position.x, 0, transform.Position.z + 4);
                                tank.transform.Rotate(Vector3.up * 180);
                                finishedTank = true;
                                finishedTanks.Add(tank);
                            }
                        }

                        info.Built = true;
                        info.IsProducing = false;
                        buildingInProgress = false;
                    }
                }
            }).Run();
      
         if (finishedOilRig)
         {
             Entities.
                 ForEach((ref AIComponent ai) =>
                 {
                     ai.NumOilRigs += 1;
                 }).Schedule();

             Entities.
                 ForEach((ref ConstructionComponent construction) =>
                 {
                     construction.EnemyOilRigs += 1;
                 }).Schedule();
         }

         if (finishedFactory)
         {
             Entities.
                 ForEach((ref AIComponent ai) =>
                 {
                     ai.NumFactories += 1;
                 }).Schedule();
         }

         if (finishedTank)
         {
             
             
                 int tankLife=0;
                 Entities.ForEach(
                     (ref SettingsComponent settings )=>
                     {
                         tankLife = settings.TankLife;

                     }
                 ).Run();
                 for (int i = 0; i < finishedTanks.Count; i++)
                 {
                     Entities.WithStructuralChanges().WithoutBurst().
                         ForEach((Entity e,ref TankEntityTag tag, in TankComponent tankComponent) =>
                         {
                             var tankEntity = EntityManager.Instantiate(e);
                             EntityManager.RemoveComponent(tankEntity, typeof(TankEntityTag));
                             EntityManager.AddComponentObject(tankEntity, new TankComponent
                             {
                                 PlayerFaction = false,
                                 Status = TankComponent.STATUS_MOVE_TO_BOARD,
                                 Tank=finishedTanks[i],
                                 Explosion = tankComponent.Explosion,
                                 RemainingLife = tankLife 
                             });
                         }).Run();
            
             }

         }
         this.buildingInProgress = buildingInProgress;
    }
     private void BuildFactory(int index)
    {
        float buildFactoryTime = 0;
        float buildTankTime = 0;
        int buildCost = 0;
        int tankCost = 0;

        float aiBuildTimeFactor = 1;

        Entities.
        ForEach(
            (ref AIComponent ai) =>
            {
                aiBuildTimeFactor = ai.AIBuildTimeFactor;
            }).Run();

        // inşa masraf ve süresi
        Entities.ForEach(
            (ref SettingsComponent settings) =>
            {
                buildFactoryTime = settings.DurationOfFactoryBuild;
                buildTankTime = settings.DurationOfTankBuild;
                buildCost = settings.CostOfFactoryBuild;
                tankCost = settings.CostOfTankBuild;
            }).Run();

        // inşaata izin verildi mi?
        bool tankApproved = false;

        Entities.
            WithoutBurst().
            ForEach(
            (in BuildingInfoComponent info) =>
            {
                if (info.Index == index && info.BuildingType == "EnemyFactory")
                {
                    if (!info.Built)
                    {
                        // fabrika yapalım
                        if (!info.IsProducing)
                        {
                            // Fabrikayı inşa edelim
                            info.TimeStartedToProduce = DateTime.Now.Ticks;
                            info.TimeToFinishProduction = info.TimeStartedToProduce + ((long)(buildFactoryTime * aiBuildTimeFactor * 10000000));
                            info.IsProducing = true;
                        }
                    }
                    else
                    {
                        // tank yapalım
                        if (!info.IsProducing)
                        {
                            info.TimeStartedToProduce = DateTime.Now.Ticks;
                            info.TimeToFinishProduction = info.TimeStartedToProduce + ((long)(buildTankTime * aiBuildTimeFactor * 10000000));
                            info.IsProducing = true;

                            tankApproved = true;
                        }
                    }
                }
            }).Run();
    }

    private void BuildOilRig(int index)
    {
        float buildOilRigTime = 0;
        int buildCost = 0;

        float aiBuildTimeFactor = 1;

        Entities.
        ForEach(
            (ref AIComponent ai) =>
            {
                aiBuildTimeFactor = ai.AIBuildTimeFactor;
            }).Run();

        // inşa masraf ve süresi
        Entities.ForEach(
            (ref SettingsComponent settings) =>
            {
                buildOilRigTime = settings.DurationOfOilRigBuild;
                buildCost = settings.CostOfOilRigBuild;
            }).Run();

        Entities.
            WithoutBurst().
            ForEach(
            (in BuildingInfoComponent info) =>
            {
                if (info.Index == index && info.BuildingType == "EnemyOilRig")
                {
                    if (!info.Built)
                    {
                        // Fabrikayı inşa edelim
                        info.TimeStartedToProduce = DateTime.Now.Ticks;
                        info.TimeToFinishProduction = info.TimeStartedToProduce + ((long)(buildOilRigTime * aiBuildTimeFactor * 10000000));
                        info.IsProducing = true;
                    }
                }
            }).Run();
    }

    private void BuildTank()
    {
        int buildingAtFactory = -1;
        float buildingTimeTank = 0;
        
        // 

        Entities.
            ForEach(
                (ref SettingsComponent settings) =>
                {
                    buildingTimeTank = settings.DurationOfTankBuild;
                    
                }).Run();
        Entities.
            WithoutBurst().
            ForEach(
                (Entity e,ref LocalTransform transform,in BuildingInfoComponent info) =>
                {
                    if (info.BuildingType=="EnemyFactory" && info.Built&& !info.IsProducing)
                    {
                        info.TimeStartedToProduce = DateTime.Now.Ticks;
                        info.TimeToFinishProduction = info.TimeStartedToProduce + (long)(buildingTimeTank * 10000000);
                        info.IsProducing = true;
                        buildingAtFactory = info.Index;

                    }
                }).Run();
                Debug.Log("AI is producing a tank at factory"+ buildingAtFactory);
    }
}
