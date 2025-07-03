using UnityEngine;

public class LowViolinProjectile : ProjectileAttack
{
    private TrailRenderer _trail;

    protected override void Init()
    {
        base.Init();

        _trail = GetComponentInChildren<TrailRenderer>();
    }

    public override void SettingAttack(float damage, float range, Vector3 dir, InstrumentsTower attacker = null)
    {
        base.SettingAttack(damage, range, dir, attacker);

        transform.right = transform.forward;
        _trail.Clear();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy.IsDead == true) return;

            Managers.Instance.Pool.PopObject(PoolType.ViolinAttackEffect, transform.position);
            enemy.Hit(_damage, _owner);
        }
    }

    protected override void ProjectileMove()
    {
        transform.position += _direction * _speed * Time.deltaTime;
    }
}
