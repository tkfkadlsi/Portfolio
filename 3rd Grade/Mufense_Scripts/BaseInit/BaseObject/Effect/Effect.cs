using UnityEngine;

[RequireComponent(typeof(PoolableObject))]
public abstract class Effect : BaseObject
{
    protected ParticleSystem _particleSystem;
    private PoolableObject _poolable;

    protected override void Init()
    {
        base.Init();

        _poolable = GetT<PoolableObject>();
        _particleSystem = GetT<ParticleSystem>();
    }

    public void PushThisObject()
    {
        _poolable.PushThisObject();
    }
}