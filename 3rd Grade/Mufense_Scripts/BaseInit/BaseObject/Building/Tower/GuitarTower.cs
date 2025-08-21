using UnityEngine;

public class GuitarTower : InstrumentsTower
{
    protected override void Init()
    {
        base.Init();

        SetTowerType(TowerType.Guitar);
    }

    protected override void Enable()
    {
        base.Enable();

        Managers.Instance.Game.GuitarPlayEvent += InstrumentsHandler;
    }

    protected override void Disable()
    {
        if (Managers.Instance != null)
        {
            Managers.Instance.Game.GuitarPlayEvent -= InstrumentsHandler;
        }

        base.Disable();
    }

    protected override void InstrumentsHandler(bool isHigh)
    {
        if (_stunBeat > 0) return;
        if (IsUpgrading) return;

        FindTarget();

        Vector3 direction;

        if (_target == null)
        {
            direction = transform.forward;
        }
        else
        {
            direction = _target.transform.position - transform.position;
        }

        direction.y = 0;

        switch (Level)
        {
            case 1:
                Level1Attack(direction, isHigh);
                break;
            case 2:
                Level2Attack(direction, isHigh);
                break;
            case 3:
                Level3Attack(direction, isHigh);
                break;
        }
    }

    protected override void FindTarget()
    {
        base.FindTarget();

        if (_count == 0)
        {
            _target = null;
            return;
        }

        int rand = Random.Range(0, _count);

        _target = _buffer[rand].GetComponent<Enemy>();
    }

    private void Level1Attack(Vector3 direction, bool isHigh)
    {
        CreateProjectile(direction, isHigh);
    }

    private void Level2Attack(Vector3 direction, bool isHigh)
    {
        Vector3 minus7p5 = Quaternion.Euler(0, -7.5f, 0) * direction;
        Vector3 plus7p5 = Quaternion.Euler(0, 7.5f, 0) * direction;

        CreateProjectile(minus7p5, isHigh);
        CreateProjectile(plus7p5, isHigh);
    }

    private void Level3Attack(Vector3 direction, bool isHigh)
    {
        Vector3 minus15 = Quaternion.Euler(0, -15f, 0) * direction;
        Vector3 plus15 = Quaternion.Euler(0, 15f, 0) * direction;

        CreateProjectile(minus15, isHigh);
        CreateProjectile(direction, isHigh);
        CreateProjectile(plus15, isHigh);
    }

    private void CreateProjectile(Vector3 direction, bool isHigh)
    {
        ProjectileAttack pa;

        if (isHigh)
        {
            pa = Managers.Instance.Pool.PopObject(PoolType.HighGuitarProjectile, transform.position).GetComponent<HighGuitarProjectile>();
        }
        else
        {
            pa = Managers.Instance.Pool.PopObject(PoolType.LowGuitarProjectile, transform.position).GetComponent<LowGuitarProjectile>();
        }

        Quaternion rotate = transform.rotation;

        Vector3 offset = transform.rotation * _offset;
        pa.transform.position += offset;

        pa.SettingAttack(_damage, _range, direction, this);
    }
}