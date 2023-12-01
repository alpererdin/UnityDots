using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public partial class IncomeSystem : SystemBase
{
    protected override void OnUpdate()
    {
        int numOilRigsPlayer = 1;
        int numOilRigsEnemy = 1;

        Entities.
            ForEach
            (
                (ref ConstructionComponent construction) =>
                {
                    numOilRigsEnemy = construction.EnemyOilRigs;
                    numOilRigsPlayer = construction.PlayerOilRigs;
                }).Run();

        Entities.
            ForEach
            (
                (ref IncomeComponent income, ref SettingsComponent settings) =>
                {
                    // oyuncu
                    if(income.LastCollectedIncomePlayer + (long)(settings.DurationOfOilRigReturn * 10000000) < DateTime.Now.Ticks)
                    { 
                        income.IncomePlayer += settings.AmounOilRigProduces * numOilRigsPlayer;
                        income.LastCollectedIncomePlayer = DateTime.Now.Ticks;
                    }

                    // düşman (AI)
                    if (income.LastCollectedIncomeEnemy + (long)(settings.DurationOfOilRigReturn * 10000000) < DateTime.Now.Ticks)
                    {
                        
                        income.IncomeEnemy += settings.AmounOilRigProduces * numOilRigsEnemy;
                        income.LastCollectedIncomeEnemy = DateTime.Now.Ticks;
                    }
                }
            ).Run();
    }
}