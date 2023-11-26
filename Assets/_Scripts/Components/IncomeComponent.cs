using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct IncomeComponent : IComponentData
{

    public long IncomePlayer;
    public long IncomeEnemy;

    public long LastCollectedIncomePlayer;
    public long LastCollectedIncomeEnemy;
    

}
