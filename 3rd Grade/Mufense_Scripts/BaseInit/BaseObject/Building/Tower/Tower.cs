using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(PoolableObject))]
public abstract class Tower : Building
{
    public int Level { get; private set; }

    public bool IsUpgrading { get; private set; }
    protected int _levelUpCooldown;

    private TowerLevelUpEffect _levelUpEffect;

    protected override void Init()
    {
        base.Init();

        SphereCollider sphere = GetT<SphereCollider>();
        sphere.isTrigger = true;

        IsUpgrading = false;
    }

    protected override void Enable()
    {
        base.Enable();

        Level = 1;
        Managers.Instance.Game.TowerChangeEvent?.Invoke();
        Managers.Instance.Game.BeatEvent += BeatHandler;
    }

    protected override void Disable()
    {
        if(Managers.Instance != null)
        {
            Managers.Instance.Game.TowerChangeEvent?.Invoke();
            Managers.Instance.Game.BeatEvent -= BeatHandler;
        }

        Level = 1;

        base.Disable();
    }

    protected virtual void BeatHandler()
    {
        if(IsUpgrading == true)
        {
            _levelUpCooldown--;

            if (_levelUpCooldown == 0)
            {
                Level++;
                IsUpgrading = false;
                _levelUpEffect.OffEffect();
                _levelUpEffect.PushThisObject();
            }
        }
    }

    public void LevelUp()
    {
        _levelUpCooldown = 32;
        IsUpgrading = true;
        _levelUpEffect = Managers.Instance.Pool.PopObject(PoolType.TowerLevelUpEffect, transform.position + new Vector3(0f, 0.05f, 0f)).GetComponent<TowerLevelUpEffect>();
        _levelUpEffect.transform.localScale = new Vector3(transform.localScale.x * 0.5f, 1f, transform.localScale.z * 0.5f);
        _levelUpEffect.OnEffect();
    }

    public void BreakTower()
    {
        IsUpgrading = false;
        _levelUpCooldown = 0;
        Level = 1;
        GetT<PoolableObject>().PushThisObject();
    }
}
