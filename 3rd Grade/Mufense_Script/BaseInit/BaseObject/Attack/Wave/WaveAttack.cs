using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using UnityEngine;

public abstract class WaveAttack : Attack
{
    [SerializeField] protected float _lifeTime;
    protected float _range;
    protected Vector3 _direction;

    public override void SettingAttack(float damage, float range, Vector3 dir, InstrumentsTower attacker = null)
    {
        base.SettingAttack(damage, range, dir, attacker);

        _range = range;
        _direction = dir.normalized;

        LifeTimeUniTask().Forget();
    }

    private async UniTask LifeTimeUniTask()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(_lifeTime));

        if (gameObject.activeInHierarchy == false) return;

        PushThisObject();
    }
}
