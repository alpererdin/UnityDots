using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

 /* public partial class ScratchDiskSystem : SystemBase
{
    private Entity scratchDiskSystemEntity;
    
    protected override void OnCreate()
    {
        scratchDiskSystemEntity = EntityManager.CreateEntity();
        
        EntityManager.AddComponent<ScratchDiskSystemTag>(scratchDiskSystemEntity);
        
        EntityManager.AddComponent<DeInitializeComponent>(scratchDiskSystemEntity);
        
      
        
        
    }

    protected override void OnUpdate()
    {
      Entities.
            WithNone<ScratchDiskSystemTag>().
            ForEach(
            (in DeInitializeComponent deinit) =>
            {
                Debug.Log("deleting stratch file" );
            }
            ).Run();

    }
}
*/