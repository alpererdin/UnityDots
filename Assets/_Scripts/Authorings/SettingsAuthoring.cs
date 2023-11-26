using Unity.Entities;
using UnityEngine;

public class SettingsAuthoring : MonoBehaviour
{
    public float DurationOfFactoryBuild=7;
    public float DurationOfOilRigBuild= 5;
    public float CostOfFactoryBuild=900;
    public float CostOfRigOilBuild=1800;
    public float DurationOfOilRigReturn=1.0f;
    public int AmounOilRigProducts=20;
}

public class SettingsBaker : Baker<SettingsAuthoring>
{
    public override void Bake(SettingsAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.None);
        AddComponent(entity,new SettingsComponent
        {
            AmounOilRigProducts = authoring.AmounOilRigProducts,
            DurationOfOilRigReturn = authoring.DurationOfOilRigReturn
        });
    }
}