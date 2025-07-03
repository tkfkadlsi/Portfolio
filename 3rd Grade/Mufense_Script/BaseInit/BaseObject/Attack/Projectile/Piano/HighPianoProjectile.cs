using UnityEngine;

public class HighPianoProjectile : ProjectileAttack
{
    private TrailRenderer _trail;

    protected override void Init()
    {
        base.Init();

        _trail = GetComponentInChildren<TrailRenderer>();
    }

    protected override void Disable()
    {
        base.Disable();

        GetT<ParticleSystem>().Stop();
    }

    public override void SettingAttack(float damage, float range, Vector3 dir, InstrumentsTower attacker = null)
    {
        base.SettingAttack(damage, range, dir, attacker);

        _trail.Clear();
        transform.right = transform.forward;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy.IsDead == true) return;

            Managers.Instance.Pool.PopObject(PoolType.PianoAttackEffect, transform.position);

            enemy.Hit(_damage, _owner);
            PushThisObject();
        }
    }

    protected override void ProjectileMove()
    {
        transform.position += _direction * _speed * Time.deltaTime;
    }
}
