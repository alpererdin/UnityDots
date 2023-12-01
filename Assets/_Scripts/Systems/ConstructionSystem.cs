using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Transforms;


public  partial class ConstructionSystem : SystemBase
{
    private DOTSDemo _demoActions;

     protected override void OnStartRunning()
    {
        _demoActions = new DOTSDemo();
        _demoActions.Enable();

        _demoActions.ConstructionBindings.BuildFactory1.performed += BuildFactory1_performed;
        _demoActions.ConstructionBindings.BuildFactory2.performed += BuildFactory2_performed;
        _demoActions.ConstructionBindings.BuildFactory3.performed += BuildFactory3_performed;
        _demoActions.ConstructionBindings.BuildFactory4.performed += BuildFactory4_performed;
        _demoActions.ConstructionBindings.BuildFactory5.performed += BuildFactory5_performed;
        _demoActions.ConstructionBindings.BuildFactory6.performed += BuildFactory6_performed;
        _demoActions.ConstructionBindings.BuildFactory7.performed += BuildFactory7_performed;
        _demoActions.ConstructionBindings.BuildFactory8.performed += BuildFactory8_performed;
        _demoActions.ConstructionBindings.BuildFactory9.performed += BuildFactory9_performed;
        _demoActions.ConstructionBindings.BuildFactory10.performed += BuildFactory10_performed;

        _demoActions.ConstructionBindings.BuildOilRig1.performed += BuildOilRig1_performed;
        _demoActions.ConstructionBindings.BuildOilRig2.performed += BuildOilRig2_performed;
        _demoActions.ConstructionBindings.BuildOilRig3.performed += BuildOilRig3_performed;
        _demoActions.ConstructionBindings.BuildOilRig4.performed += BuildOilRig4_performed;
        _demoActions.ConstructionBindings.BuildOilRig5.performed += BuildOilRig5_performed;
        
        Entities.
            WithoutBurst().
            ForEach(
                (ref LocalTransform transform,in BuildingInfoComponent buildingInfo) =>
                {
                    if (buildingInfo.Built)
                    {
                
                        buildingInfo.SceneObject = GameObject.Instantiate(buildingInfo.Prefab, transform.Position, quaternion.identity);
                        buildingInfo.SceneObject.transform.localScale = new Vector3(.67f, .67f, .34f);
                    }
                }
                ).Run();
    }

   


    #region inputs

    private void BuildFactory1_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        BuildFactory(0);
    }
    private void BuildFactory2_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        BuildFactory(1);
    }
    private void BuildFactory3_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        BuildFactory(2);
    }
    private void BuildFactory4_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        BuildFactory(3);
    }
    private void BuildFactory5_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        BuildFactory(4);
    }
    private void BuildFactory6_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        BuildFactory(5);
    }
    private void BuildFactory7_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        BuildFactory(6);
    }
    private void BuildFactory8_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        BuildFactory(7);
    }
    private void BuildFactory9_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        BuildFactory(8);
    }
    private void BuildFactory10_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        BuildFactory(9);
    }

    private void BuildOilRig1_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        BuildOilRig(0);
    }
    private void BuildOilRig2_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        BuildOilRig(1);
    }
    private void BuildOilRig3_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        BuildOilRig(2);
    }
    private void BuildOilRig4_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        BuildOilRig(3);
    }
    private void BuildOilRig5_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        BuildOilRig(4);
    }



    #endregion
    

    private void BuildFactory(int index)
    {
        float buildFactoryTime = 0;
        float buildTankTime = 0;
        long playerIncome = 0;
        int buildCost=0;
        int tankCost=0;
        Entities.ForEach(
            (ref SettingsComponent settings )=>
            {
                buildFactoryTime = settings.DurationOfFactoryBuild;
                buildCost = settings.CostOfFactoryBuild;
                buildTankTime = settings.DurationOfTankBuild;
                tankCost = settings.CostOfTankBuild;

            }
        ).Run();
        
        Entities.ForEach(
            (ref IncomeComponent income) =>
            {
                playerIncome = income.IncomePlayer;
                
            }).Run();

        bool buildingApproved = false;
        bool tankApproved = false;
       Entities.WithoutBurst()
           .ForEach(
           (in BuildingInfoComponent info)=>
       {
           if (info.Index == index && info.BuildingType == "PlayerFactory")
           {
               if (!info.Built)
               {
                   if (buildCost <= playerIncome && !info.IsProducing)
                   {
                       //
                       info.TimeStartedToProduce = DateTime.Now.Ticks;
                       info.TimeToFinishProduction =info.TimeStartedToProduce + ((long)(buildFactoryTime * 10000000));
                       info.IsProducing = true;
                       buildingApproved = true;
                   }
               }
               else
               {
                   if (buildCost<= playerIncome && !info.IsProducing)
                   {
                       info.TimeStartedToProduce = DateTime.Now.Ticks;
                       info.TimeToFinishProduction = info.TimeStartedToProduce + ((long)(buildTankTime * 10000000));
                       info.IsProducing = true;

                       tankApproved = true;
                   }
               }
           }
       }).Run();
       if (buildingApproved)
       {
           
           Entities.ForEach(
               (ref IncomeComponent income) =>
               {
                   income.IncomePlayer -= buildCost;
               }).Run();
       }
       if (tankApproved)
       {
           
           Entities.ForEach(
               (ref IncomeComponent income) =>
               {
                   income.IncomePlayer -= tankCost;
               }).Run();
       }

    }
    private void BuildOilRig(int index)
    {
        float buildOilRigTime = 0;
        long playerIncome = 0;
        int buildCost=0;
        Entities.ForEach(
            (ref SettingsComponent settings )=>
            {
                buildOilRigTime = settings.DurationOfOilRigBuild;
                buildCost = settings.CostOfOilRigBuild;

            }
        ).Run();
        Entities.ForEach(
            (ref IncomeComponent income) =>
            {
                playerIncome = income.IncomePlayer;
                
            }).Run();
        bool buildingApproved = false;
        Entities.WithoutBurst()
            .ForEach(
                (in BuildingInfoComponent info)=>
                {
                    if (info.Index == index && info.BuildingType == "PlayerOilRig")
                    {
                        if (!info.Built && buildCost <=playerIncome)
                        {
                            //
                            info.TimeStartedToProduce = DateTime.Now.Ticks;
                            info.TimeToFinishProduction =info.TimeStartedToProduce + ((long)(buildOilRigTime * 10000000));
                            info.IsProducing = true;
                            buildingApproved = true;
                        }
                    }
                }).Run();
        if (buildingApproved)
        {
        Entities.ForEach(
            (ref IncomeComponent income) =>
            {
                income.IncomePlayer -= buildCost;
            }).Run();
        }
    } 
 
    protected override void OnUpdate()
    {
        bool finishedOilRig = false;
        //bool finishedFactory = false;
        bool finishedATank = false;
        List<GameObject> finishedTanks = new List<GameObject>();
         Entities.WithoutBurst().ForEach(
             (ref LocalTransform transform,in BuildingInfoComponent info) =>
         {
             if (info.IsProducing&& info.BuildingType!="EnemyFactory"&&info.BuildingType!="EnemyOilRig")
             {
                 if (info.TimeToFinishProduction < DateTime.Now.Ticks)
                 {
                     info.IsProducing = false;
                   
                     
                     if (!info.Built && info.BuildingType=="PlayerOilRig")
                     {
                         info.SceneObject = GameObject.Instantiate(info.Prefab,transform.Position,quaternion.identity);
                         info.SceneObject.transform.localScale = new Vector3(0.67f, 0.67f, 0.34f);
                         finishedOilRig = true;
                     }
                     else if(info.BuildingType == "PlayerFactory")
                     {
                         // oyuncunun fabrikası VEYA tankı
                         if(!info.Built)
                         {
                             // fabrika
                             info.SceneObject = GameObject.Instantiate(info.Prefab, transform.Position, quaternion.identity);
                             info.SceneObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                           //  info.SceneObject.transform.Rotate(Vector3.up * 90);
                         } else
                         {
                             // tank
                             var tank = GameObject.Instantiate(info.TankPrefab);
                             tank.transform.position = new Vector3(transform.Position.x + 4, 0, transform.Position.z);
                             finishedATank = true;
                             finishedTanks.Add(tank);
                         }
                     }
                     info.Built = true;
                 }
                 else
                 {
                     float value = (info.TimeToFinishProduction - DateTime.Now.Ticks) / 10000000;
                 }
                
                 
             }
             
         }).Run();
         if (finishedOilRig)
         {
             Entities.ForEach(
                 (ref ConstructionComponent construction) =>
                 {
                     construction.PlayerOilRigs += 1;
                 }).Run();

         }
         //

         if (finishedATank)
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
                                        PlayerFaction = true,
                                        Status = TankComponent.STATUS_MOVE_TO_BOARD,
                                        Tank=finishedTanks[i],
                                        Explosion = tankComponent.Explosion,
                                        RemainingLife = tankLife
                                });
                        }
                    ).Run();
             }
           
         }
    }
}















