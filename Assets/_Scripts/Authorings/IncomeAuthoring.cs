 
    using Unity.Entities;
    using UnityEngine;

    public class IncomeAuthoring : MonoBehaviour
    {
        public long IncomePlayer= 3000;
        public long IncomeEnemy = 3000;

    }

    public class IncomeBaker : Baker<IncomeAuthoring>
    {
        public override void Bake(IncomeAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            
            AddComponent(entity,new IncomeComponent
            {
                IncomePlayer = authoring.IncomePlayer,
                IncomeEnemy = authoring.IncomeEnemy,
                LastCollectedIncomeEnemy = 0,
                LastCollectedIncomePlayer = 0
            });
        }
    }
 