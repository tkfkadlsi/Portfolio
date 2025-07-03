using UnityEngine;

public class LowGuitarProjectile : ProjectileAttack
{
    public override void SettingAttack(float damage, float range, Vector3 dir, InstrumentsTower attacker = null)
    {
        base.SettingAttack(damage, range, dir, attacker);

        transform.right = transform.forward;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            if (other.CompareTag("Enemy"))
            {
                Enemy enemy = other.GetComponent<Enemy>();
                if (enemy.IsDead == true) return;

                Vector3 position = enemy.transform.position;

                position.y = transform.position.y;

                Managers.Instance.Pool.PopObject(PoolType.GuitarAttackEffect, position);

                enemy.Hit(_damage, _owner);
            }
        }
    }

    protected override void ProjectileMove()
    {
        transform.position += _direction * _speed * Time.deltaTime;
    }
}
