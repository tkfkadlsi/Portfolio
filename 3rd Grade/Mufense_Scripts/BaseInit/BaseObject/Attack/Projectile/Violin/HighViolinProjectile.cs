using UnityEngine;

public class HighViolinProjectile : ProjectileAttack
{
    private TrailRenderer _trail;
    private float _stunTime;

    protected override void Init()
    {
        base.Init();

        _trail = GetComponentInChildren<TrailRenderer>();
    }

    public override void SettingAttack(float damage, float range, Vector3 dir, InstrumentsTower attacker = null)
    {
        base.SettingAttack(damage, range, dir, attacker);

        if(attacker != null)
        {
            _stunTime = (attacker.Level - 1) * 0.1f;
        }

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

            if(enemy.IsDead == true) return;

            if(_stunTime >= 0.01f)
            {
                enemy.Stun(_stunTime);
            }
        }
    }

    protected override void ProjectileMove()
    {
        transform.position += _direction * _speed * Time.deltaTime;
    }
}
