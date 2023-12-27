using Unity.Entities;
using UnityEngine;

public class BattleFieldAuthoring : MonoBehaviour
{
    public int Width;
    public int Height;
    public Transform BattleFieldPlayer;
    public Transform BattleFieldEnemy;
}

public class BattleFieldBaker : Baker<BattleFieldAuthoring>
{
    public override void Bake(BattleFieldAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.None);

        AddComponent(entity, new BattleFieldComponent
        {
            BattleStarted = false,
            Width = authoring.Width,
            Height = authoring.Height,
            NextX = 0,
            NextY = 0,
            EnemyFieldX = authoring.BattleFieldEnemy.position.x,
            EnemyFieldY = authoring.BattleFieldEnemy.position.z,
            PlayerFieldX = authoring.BattleFieldPlayer.position.x,
            PlayerFieldY = authoring.BattleFieldPlayer.position.z
        });
    }
}