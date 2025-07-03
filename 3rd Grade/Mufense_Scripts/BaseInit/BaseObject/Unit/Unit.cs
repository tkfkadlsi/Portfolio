using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(PoolableObject))]
public abstract class Unit : BaseObject, IHitable, IStunable, IDieable
{
    private float _maxHp;
    private float _hp;
    private bool _isStun;
    private bool _isDead;

    public float MaxHP { get { return _maxHp; } }
    public float HP { get { return _hp; } }
    public bool IsStun { get { return _isStun; } }
    public bool IsDead {  get { return _isDead; } }

    protected override void Enable()
    {
        base.Enable();

        _isDead = false;
    }

    public void SetHP(float hp)
    {
        _maxHp = hp;
        _hp = hp;
    }

    public virtual void Hit(float damage, InstrumentsTower attacker = null)
    {
        _hp -= damage;

        if (_hp <= 0f)
        {
            Die(attacker);
        }
    }

    public virtual void Stun(float time)
    {
        if(_isStun == false)
        {
            StunUniTask(time).Forget();
        }
    }

    private async UniTask StunUniTask(float time)
    {
        _isStun = true;
        GetT<NavMeshAgent>().isStopped = true;

        await UniTask.Delay(TimeSpan.FromSeconds(time));

        if (gameObject.activeInHierarchy == false) return;

        GetT<NavMeshAgent>().isStopped = false;
        _isStun = false;
    }

    public virtual void Die(InstrumentsTower attacker = null)
    {
        if (_isDead) return;

        _isDead = true;

        GetT<PoolableObject>().PushThisObject();
    }

    public void Heal(float value)
    {
        _hp += value;
        if(_hp > _maxHp)
        {
            _hp = _maxHp;
        }
    }
}
