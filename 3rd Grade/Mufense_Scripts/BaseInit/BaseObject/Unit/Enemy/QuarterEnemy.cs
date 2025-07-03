using UnityEngine;

public class QuarterEnemy : Enemy
{
    protected override void Init()
    {
        base.Init();

        SetEnemyType(EnemyType.QuarterEnemy);
    }
}
