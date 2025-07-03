using UnityEngine;

public class QuarterRestEnemy : Enemy
{
    protected override void Init()
    {
        base.Init();

        SetEnemyType(EnemyType.QuarterRestEnemy, true);
    }

    public override void Die(InstrumentsTower attacker = null)
    {
        if (attacker != null)
        {
            attacker.Stun((int)Type - 10);
        }

        base.Die(attacker);
    }
} 