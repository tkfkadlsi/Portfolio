using UnityEngine;

public class DrumTower : InstrumentsTower
{
    protected override void Init()
    {
        base.Init();

        SetTowerType(TowerType.Drum);
    }

    protected override void Enable()
    {
        base.Enable();

        Managers.Instance.Game.DrumPlayEvent += InstrumentsHandler;
    }

    protected override void Disable()
    {
        if (Managers.Instance != null)
        {
            Managers.Instance.Game.DrumPlayEvent -= InstrumentsHandler;
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

        WaveAttack wave;

        if (isHigh)
        {
            wave = Managers.Instance.Pool.PopObject(PoolType.HighDrumWave, transform.position + _offset).GetComponent<HighDrumWave>();
        }
        else
        {
            wave = Managers.Instance.Pool.PopObject(PoolType.LowDrumWave, transform.position + _offset).GetComponent<LowDrumWave>();
        }

        wave.SettingAttack(_damage, _range, Vector3.zero, this);
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
