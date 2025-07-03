using UnityEngine;
using System.Collections.Generic;

public class VocalTower : Tower
{
    public readonly TowerType Type = TowerType.Vocal;

    [SerializeField] private LayerMask _instrumentsTowerLayer;

    private List<InstrumentsTower> _instruments = new List<InstrumentsTower>();
    private Collider[] _buffer = new Collider[20];

    protected override void Enable()
    {
        base.Init();

        Managers.Instance.Game.VocalPlayEvent += VocalHandler;
        Managers.Instance.Game.TowerChangeEvent += FindTower;
    }

    protected override void Disable()
    {
        if(Managers.Instance != null)
        {
            Managers.Instance.Game.VocalPlayEvent -= VocalHandler;
            Managers.Instance.Game.TowerChangeEvent -= FindTower;
        }

        base.Disable();
    }

    private void VocalHandler(float time)
    {
        foreach(var inst in _instruments)
        {
            inst.AddDamageMultiplier(Managers.Instance.Data.TowerDatas[Type].Damage[Level], time);
        }
    }

    private void FindTower()
    {
        _instruments.Clear();

        int count = Physics.OverlapSphereNonAlloc(transform.position, Managers.Instance.Data.TowerDatas[Type].Range[Level], _buffer, _instrumentsTowerLayer);

        for(int i = 0; i < count; i++)
        {
            if (_buffer[i].TryGetComponent(out InstrumentsTower inst))
            {
                _instruments.Add(inst);
            }
        }
    }
}
