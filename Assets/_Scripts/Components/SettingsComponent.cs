using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct SettingsComponent : IComponentData
{
   public float DurationOfFactoryBuild;  
   public float DurationOfOilRigBuild;  
   public float DurationOfTankBuild; 
   public int CostOfFactoryBuild;  
   public int CostOfOilRigBuild;  
   public int CostOfTankBuild; 
   public float DurationOfOilRigReturn;  
   public int AmounOilRigProduces; 
   public float TankSpeed; //  
   public float TankFireDistance; // 
   public float BulletSpeed; //  
   public float TankReloadTime; //  
   public int TankLife; // 

  
}
