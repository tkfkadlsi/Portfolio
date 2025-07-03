using UnityEngine;
using UnityEngine.AI;

public class EnemyEffect : MonoBehaviour
{
    private Enemy _owner;

    public void SetOwner(Enemy owner)
    {
        _owner = owner;
    }

    public void HighSpeedEffect(NavMeshAgent agnet)
    {
        agnet.speed *= 1.2f;
    }

    public void DefenderEffect()
    {
        Managers.Instance.Pool.PopObject(PoolType.DefendEffect, transform, transform.position);
    }

    public void HealEffect()
    {
        Managers.Instance.Pool.PopObject(PoolType.HealEffect, transform, transform.position);
    }
}
