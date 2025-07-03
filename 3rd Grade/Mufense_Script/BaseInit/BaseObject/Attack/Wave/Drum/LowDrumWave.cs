using UnityEngine;

public class LowDrumWave : WaveAttack
{
    private ParticleSystem _particleSystem;

    protected override void Init()
    {
        base.Init();

        _particleSystem = GetT<ParticleSystem>();
    }

    public override void SettingAttack(float damage, float range, Vector3 dir, InstrumentsTower attacker = null)
    {
        damage *= 0.1f;

        base.SettingAttack(damage, range, dir, attacker);

        transform.localScale = Vector3.one * _range; 
        _particleSystem.Play();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))      
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy.IsDead == true) return;

            Vector3 offset = (enemy.transform.position - transform.position).normalized * 0.15f;
            Vector3 position = enemy.transform.position - offset;
            position.y = transform.position.y;

            Managers.Instance.Pool.PopObject(PoolType.DrumAttackEffect, position);

            enemy.Hit(_damage, _owner);
        }
    }
}
