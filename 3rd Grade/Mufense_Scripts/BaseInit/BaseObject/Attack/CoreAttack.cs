using Cysharp.Threading.Tasks;
using UnityEngine;

public class CoreAttack : BaseObject
{
    private readonly float _damage = 5f;
    private Enemy _target;

    public void SettingAttack(Enemy target)
    {
        _target = target;
        LifeTime().Forget();
    }

    private void Update()
    {
        if(gameObject.activeInHierarchy == true && _target != null)
        {
            transform.position = _target.transform.position;
        }
    }

    private async UniTask LifeTime()
    {
        await UniTask.Delay(1000);

        Managers.Instance.Pool.PopObject(PoolType.CoreAttackEffect, transform.position);
        _target.Hit(_damage);

        await UniTask.Delay(1000);

        GetT<PoolableObject>().PushThisObject();
    }
}
