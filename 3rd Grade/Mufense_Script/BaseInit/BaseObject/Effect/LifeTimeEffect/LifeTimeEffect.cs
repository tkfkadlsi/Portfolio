using UnityEngine;
using System.Collections;
using Cysharp.Threading.Tasks;
using System;

public class LifeTimeEffect : Effect
{
    [SerializeField] protected float _lifeTime;

    protected override void Enable()
    {
        base.Enable();

        _particleSystem.Play();

        LifeTimeUniTask().Forget();
    }

    private async UniTask LifeTimeUniTask()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(_lifeTime));

        if (gameObject.activeInHierarchy == false) return;

        PushThisObject();
    }
}
