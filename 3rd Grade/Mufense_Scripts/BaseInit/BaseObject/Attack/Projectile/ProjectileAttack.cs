using Cysharp.Threading.Tasks;
using System.Collections;
using UnityEngine;

public abstract class ProjectileAttack : Attack
{
    [SerializeField] protected float _speed;
    protected Vector3 _direction;
    private float _lifeTime;
    private float _t;


    public override void SettingAttack(float damage, float range, Vector3 dir, InstrumentsTower attacker = null)
    {
        base.SettingAttack(damage, range, dir, attacker);

        _lifeTime = range / _speed;
        _direction = dir.normalized;

        transform.forward = _direction;

        LifeUniTask().Forget();
    }

    private async UniTask LifeUniTask()
    {
        _t = 0f;
        while(_t < _lifeTime)
        {
            ProjectileMove();

            await UniTask.Yield();

            if (gameObject.activeInHierarchy == false) return;

            _t += Time.deltaTime;
        }

        PushThisObject();
    }

    protected float GetRemainderLifetime()
    {
        return _lifeTime - _t;
    }

    protected abstract void ProjectileMove();
}
