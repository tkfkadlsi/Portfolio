using UnityEngine;
using UnityEngine.Rendering;

public class GuitarTower : InstrumentsTower
{
    protected override void Init()
    {
        base.Init();

        SetInstrumentsType(TowerType.Guitar);
    }

    protected override void Enable()
    {
        base.Enable();

        Managers.Instance.Game.GuitarPlayEvent += InstrumentsHandler;
    }

    protected override void Disable()
    {
        if(Managers.Instance != null)
        {
            Managers.Instance.Game.GuitarPlayEvent -= InstrumentsHandler;
        }

        base.Disable();
    }

    protected override void InstrumentsHandler(bool isHigh)
    {
        if (_stunBeat > 0) return;
        if (IsUpgrading) return;
        if (Managers.Instance.Game.GetComponentInScene<MusicPowerData>()
            .RemoveMusicPower(Managers.Instance.Data.TowerDatas[Type].UsingMusicPower[Level]) == false) return;

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

        ProjectileAttack pa;

        if (isHigh)
        {
            pa = Managers.Instance.Pool.PopObject(PoolType.HighGuitarProjectile, transform.position).GetComponent<HighGuitarProjectile>();
        }
        else
        {
            pa = Managers.Instance.Pool.PopObject(PoolType.LowGuitarProjectile, transform.position).GetComponent<LowGuitarProjectile>();
        }

        Vector3 offset = transform.rotation * _offset;
        pa.transform.position += offset;

        pa.SettingAttack(_damage, _range, direction, this);
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
}