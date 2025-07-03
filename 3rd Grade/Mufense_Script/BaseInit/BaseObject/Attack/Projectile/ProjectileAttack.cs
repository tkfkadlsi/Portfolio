using Cysharp.Threading.Tasks;
using System.Collections;
using UnityEngine;

public abstract class ProjectileAttack : Attack
{
    [SerializeField] protected float _speed;
    protected Vector3 _direction;
    private float _lifeTime;


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
        float t = 0f;
        while(t < _lifeTime)
        {
            ProjectileMove();

            await UniTask.Yield();

            if (gameObject.activeInHierarchy == false) return;

            t += Time.deltaTime;
        }

        PushThisObject();
    }

    protected abstract void ProjectileMove();
}
