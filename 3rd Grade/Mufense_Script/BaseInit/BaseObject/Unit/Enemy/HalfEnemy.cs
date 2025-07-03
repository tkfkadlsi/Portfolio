using UnityEngine;

public class HalfEnemy : Enemy
{
    protected override void Init()
    {
        base.Init();

        SetEnemyType(EnemyType.HalfEnemy);
    }
}
