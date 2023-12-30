using Unity.Entities;

public struct BattleFieldComponent : IComponentData
{
    public int Width;
    public int Height;
    public int NextX;
    public int NextY;
    public float PlayerFieldX;
    public float PlayerFieldY;
    public float EnemyFieldX;
    public float EnemyFieldY;
    public int NumAITanks;
    public int NumPlayerTanks;
    public bool BattleStarted;
}