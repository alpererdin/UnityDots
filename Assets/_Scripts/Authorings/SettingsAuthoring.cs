using Unity.Entities;
using UnityEngine;

public class SettingsAuthoring : MonoBehaviour
{
    public float DurationOfFactoryBuild = 7;
    public float DurationOfTankBuild = 6;
    public float DurationOfOilRigBuild = 5;
    public int CostOfFactoryBuild = 900;
    public int CostOfOilRigBuild = 1800;
    public int CostOfTankBuild = 100;
    public float DurationOfOilRigReturn = 1.0f;
    public int AmounOilRigProduces = 20;
    public float TankSpeed = 3;
    public float TankFireDistance = 6;
    public float BulletSpeed = 30;
    public float TankReloadTime = 1.4f;
    public int TankLife = 3;
}

public class SettingsBaker : Baker<SettingsAuthoring>
{
    public override void Bake(SettingsAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.None);
        AddComponent(entity,new SettingsComponent
        {
            DurationOfFactoryBuild = authoring.DurationOfFactoryBuild,
            DurationOfTankBuild = authoring.DurationOfTankBuild,
            DurationOfOilRigBuild = authoring.DurationOfOilRigBuild,
            CostOfFactoryBuild = authoring.CostOfFactoryBuild,
            CostOfOilRigBuild = authoring.CostOfOilRigBuild,
            CostOfTankBuild = authoring.CostOfTankBuild,
            AmounOilRigProduces = authoring.AmounOilRigProduces,
            DurationOfOilRigReturn = authoring.DurationOfOilRigReturn,
            TankFireDistance = authoring.TankFireDistance,
            TankReloadTime = authoring.TankReloadTime,
            TankSpeed = authoring.TankSpeed,
            TankLife = authoring.TankLife,
            BulletSpeed = authoring.BulletSpeed,
            
          
        });
    }
}