using UnityEngine;

public class TrumpetTower : InstrumentsTower
{
    protected override void Init()
    {
        base.Init();

        SetTowerType(TowerType.Trumpet);
    }

    protected override void Enable()
    {
        base.Enable();

        Managers.Instance.Game.TrumpetPlayEvent += InstrumentsHandler;
    }

    protected override void Disable()
    {
        if(Managers.Instance != null)
        {
            Managers.Instance.Game.TrumpetPlayEvent -= InstrumentsHandler;
        }

        base.Disable();
    }

    protected override void InstrumentsHandler(bool isHigh)
    {
        if (_stunBeat > 0) return;
        if (IsUpgrading) return;
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

        if (isHigh)
        {
            Managers.Instance.Pool.PopObject(PoolType.HighTrumpetWave, transform.position);
        }
        else
        {
            Managers.Instance.Pool.PopObject(PoolType.LowTrumpetWave, transform.position);
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
