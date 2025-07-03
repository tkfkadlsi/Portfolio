using UnityEngine;

public class ViolinTower : InstrumentsTower
{
    protected override void Init()
    {
        base.Init();

        SetInstrumentsType(TowerType.Violin);
    }

    protected override void Enable()
    {
        base.Enable();

        Managers.Instance.Game.ViolinPlayEvent += InstrumentsHandler;
    }

    protected override void Disable()
    {
        if(Managers.Instance != null)
        {
            Managers.Instance.Game.ViolinPlayEvent -= InstrumentsHandler;
        }

        base.Disable();
    }

    protected override void InstrumentsHandler(bool isHigh)
    {
        if (_stunBeat > 0) return;
        if (IsUpgrading) return;
        if (Managers.Instance.Game.GetComponentInScene<MusicPowerData>()
            .RemoveMusicPower(Managers.Instance.Data.TowerDatas[Type].UsingMusicPower[Level]) == false) return;

        if (_target == null || _target.gameObject.activeInHierarchy == false || Vector3.Distance(_target.transform.position, transform.position) > _range)
        {
            FindTarget();
        }

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
            pa = Managers.Instance.Pool.PopObject(PoolType.HighViolinWave, transform.position).GetComponent<ProjectileAttack>();
        }
        else
        {
            pa = Managers.Instance.Pool.PopObject(PoolType.LowViolinWave, transform.position).GetComponent<ProjectileAttack>();
        }

        pa.transform.position += _offset;
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

        float minDistance = float.MaxValue;
        int minObjectId = 0;

        for (int i = 0; i < _count; i++)
        {
            float distance = (_buffer[i].transform.position - transform.position).sqrMagnitude;

            if (distance < minDistance)
            {
                minDistance = distance;
                minObjectId = i;
            }
        }

        _target = _buffer[minObjectId].GetComponent<Enemy>();
    }
}
