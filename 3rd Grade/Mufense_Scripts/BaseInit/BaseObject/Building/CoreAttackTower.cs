using System.Collections.Generic;
using UnityEngine;

public class CoreAttackTower : BaseObject
{
    private List<Enemy> _targetList = new List<Enemy>();

    private bool _canAttack;

    protected override void Init()
    {
        base.Init();
        _canAttack = false; 
    }

    protected override void Enable()
    {
        base.Enable();

        Managers.Instance.Game.CorePlayEvent += CoreAttackHandler;
    }

    protected override void Disable()
    {
        base.Disable();

        if(Managers.Instance != null)
        {
            Managers.Instance.Game.CorePlayEvent -= CoreAttackHandler;
        }
    }

    private void CoreAttackHandler(bool isHigh)
    {
        if (_canAttack == false) return;
        if (_targetList.Count == 0) return;

        ShuffleTargetList();

        int repeat = Mathf.Min(5, _targetList.Count);

        for(int i = 0; i < repeat; i++)
        {
            Enemy target = _targetList[i];

            CoreAttack coreAttack = Managers.Instance.Pool.PopObject(PoolType.CoreAttack, target.transform.position).GetComponent<CoreAttack>();
            coreAttack.SettingAttack(target);
        }
    }

    private void ShuffleTargetList()
    {
        int random1 = 0;
        int random2 = 0;

        for(int i = 0; i < 50; i++)
        {
            random1 = Random.Range(0, _targetList.Count);
            random2 = Random.Range(0, _targetList.Count);

            Enemy swapEnemy = _targetList[random1];
            _targetList[random1] = _targetList[random2];
            _targetList[random2] = swapEnemy;
        }
    }

    public void AddTarget(Enemy target)
    {
        _targetList.Add(target);
    }

    public void RemoveTarget(Enemy target)
    {
        _targetList.Remove(target);
    }

    public void SetCanAttack(bool can)
    {
        _canAttack = can;
    }
}
