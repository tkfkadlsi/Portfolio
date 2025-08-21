using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(PoolableObject))]
public abstract class Tower : Building
{
    public int Level { get; private set; }
    public TowerType Type { get; private set; }
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
                IsUpgrading = false;
                LevelUp();
            }
        }
    }

    protected void SetTowerType(TowerType type)
    {
        Type = type;
    }

    public void LevelUpStart()
    {
        _levelUpCooldown = 32;
        IsUpgrading = true;
        _levelUpEffect = Managers.Instance.Pool.PopObject(PoolType.TowerLevelUpEffect, transform.position + new Vector3(0f, 0.05f, 0f)).GetComponent<TowerLevelUpEffect>();
        _levelUpEffect.transform.localScale = new Vector3(transform.localScale.x * 0.5f, 1f, transform.localScale.z * 0.5f);
        _levelUpEffect.OnEffect();
    }

    private void LevelUp()
    {
        Level++;
        Managers.Instance.UI.GetRootUI().GetCanvas<TowerInfoCanvas>().SyncUI(this);
        _levelUpEffect.OffEffect();
        _levelUpEffect.PushThisObject();
    }

    public void BreakTower()
    {
        IsUpgrading = false;
        _levelUpCooldown = 0;
        Level = 1;
        Managers.Instance.Data.TowerCountManagement.BreakTower(Type);
        GetT<PoolableObject>().PushThisObject();
    }
}
