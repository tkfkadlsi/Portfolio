using UnityEngine;

public class PianoTower : InstrumentsTower
{
    protected override void Init()
    {
        base.Init();

        SetInstrumentsType(TowerType.Piano);
    }

    protected override void Enable()
    {
        base.Enable();

        Managers.Instance.Game.PianoPlayEvent += InstrumentsHandler;
    }

    protected override void Disable()
    {
        if (Managers.Instance != null)
        {
            Managers.Instance.Game.PianoPlayEvent -= InstrumentsHandler;
        }

        base.Disable();
    }

    protected override void InstrumentsHandler(bool isHigh)
    {
        if (_stunBeat > 0) return;
        if(IsUpgrading) return;
        if (Managers.Instance.Game.GetComponentInScene<MusicPowerData>()
            .RemoveMusicPower(Managers.Instance.Data.TowerDatas[Type].UsingMusicPower[Level]) == false) return;

        if (_target == null || _target.gameObject.activeInHierarchy == false || Vector3.Distance(_target.transform.position, transform.position) > _range)
        {
            FindTarget();
        }

        int rand = Random.Range(1, 5);

        ProjectileAttack pa;

        Vector3 direction;

        if(_target == null)
        {
            direction = transform.forward;
        }
        else
        {
            direction = _target.transform.position - transform.position;
        }

        direction.y = 0;

        for (int i = 0; i < rand; i++)
        {
            if (isHigh)
            {//HighPiano => Piano is main melody instruments
                pa = Managers.Instance.Pool.PopObject(PoolType.HighPianoProjectile, transform.position).GetComponent<HighPianoProjectile>();
            }
            else
            {//LowPiano => Piano is not main melody instruments
                pa = Managers.Instance.Pool.PopObject(PoolType.LowPianoProjectile, transform.position).GetComponent<LowPianoProjectile>();
            }

            Vector3 offset = transform.rotation * _offset;
            pa.transform.position += offset;

            pa.transform.position += (Quaternion.Euler(0, 90, 0) * direction).normalized * Random.Range(-0.8f, 0.8f);
            pa.SettingAttack(_damage, _range, direction, this);
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
