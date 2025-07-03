using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(PoolableObject))]
public abstract class Attack : BaseObject
{
    [SerializeField] private float _hitRadius = 0.5f;

    protected InstrumentsTower _owner;
    protected float _damage;

    private PoolableObject _poolable;

    protected override void Init()
    {
        base.Init();

        _poolable = GetT<PoolableObject>();

        SphereCollider sphere = GetT<SphereCollider>();
        sphere.radius = _hitRadius;
        sphere.isTrigger = true;
    }

    public virtual void SettingAttack(float damage, float range, Vector3 dir, InstrumentsTower attacker = null)
    {
        _damage = damage;
        _owner = attacker;
    }

    public void PushThisObject()
    {
        _poolable.PushThisObject();
    }

    protected abstract void OnTriggerEnter(Collider other);
}
