using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(EnemyEffect))]
public abstract class Enemy : Unit
{
    private readonly float _arriveDistance = 5f;

    public EnemyType Type { get; private set; }

    private int _beatsPerMove;
    private int _beatCount;
    private bool _isLastJump;
    private EnemyVisual _visual;
    private EnemyEffect _effect;
    private Vector3 _targetPos;

    protected override void Init()
    {
        base.Init();

        _visual = GetComponentInChildren<EnemyVisual>();
        _effect = GetT<EnemyEffect>();

        CapsuleCollider capsule = GetT<CapsuleCollider>();
        capsule.isTrigger = false;
        capsule.radius = 0.5f;
        capsule.height = 3.5f;

        Core core = Managers.Instance.Game.GetComponentInScene<Core>();
        _targetPos = core.transform.position;
    }

    protected override void Enable()
    {
        base.Enable();

        NavMeshAgent agent = GetT<NavMeshAgent>();

        agent.agentTypeID = 0;
        agent.acceleration = 1000f;
        agent.speed = 4f;
        agent.angularSpeed = 360f;
        agent.stoppingDistance = 0f;
        agent.autoBraking = false;
        agent.isStopped = true;

        _isLastJump = false;

        Managers.Instance.Game.BeatEvent += BeatCounter;
    }

    protected override void Disable()
    {
        if (Managers.Instance != null)
        {
            Managers.Instance.Game.BeatEvent -= BeatCounter;
        }

        DOTween.Kill(gameObject);
        DOTween.Kill(_visual.gameObject);
        base.Disable();
    }

    #region Setting

    protected void SetEnemyType(EnemyType type, bool isRest = false)
    {
        Type = type;

        if (isRest)
        {
            _beatsPerMove = (int)Type - 10;
            _beatCount = (int)Type - 10;
        }
        else
        {
            _beatsPerMove = (int)Type;
            _beatCount = (int)Type;
        }
    }

    public void SetEffectType(EffectType type)
    {
        _visual.SetVisualColor(type);

        switch (type)
        {
            case EffectType.HighSpeed:
                {
                    _effect.HighSpeedEffect(GetT<NavMeshAgent>());
                }
                break;
            case EffectType.Defend:
                {
                    _effect.DefenderEffect();
                }
                break;
            case EffectType.Heal:
                {
                    _effect.HealEffect();
                }
                break;
        }
    }

    public void AddTarget()
    {
        Managers.Instance.Game.GetComponentInScene<CoreAttackTower>().AddTarget(this);
    }

    private void RemoveTarget()
    {
        Managers.Instance.Game.GetComponentInScene<CoreAttackTower>().RemoveTarget(this);
    }

    #endregion

    #region Movement

    private void BeatCounter()
    {
        _beatCount++;

        if (_beatCount > _beatsPerMove)
        {
            if (_isLastJump)
            {
                _beatCount -= _beatsPerMove;
                LastJump().Forget();
            }
            {
                _beatCount -= _beatsPerMove;
                Jump().Forget();
            }
        }
    }

    private async UniTask Jump()
    {
        if (IsStun == true) return;

        NavMeshAgent agent = GetT<NavMeshAgent>();
        float jumpTime = Managers.Instance.Game.UnitTime * _beatsPerMove * 0.5f;

        agent.isStopped = false;
        agent.SetDestination(_targetPos);

        _visual.transform.DOLocalJump(Vector3.zero, _beatsPerMove, 1, jumpTime);

        await UniTask.Delay(TimeSpan.FromSeconds(jumpTime * 1.1f));

        if (gameObject.activeInHierarchy == false) return;

        agent.isStopped = true;
        agent.velocity = Vector3.zero;
    }

    private async UniTask LastJump()
    {
        Core core = Managers.Instance.Game.GetComponentInScene<Core>();
        float jumpTime = Managers.Instance.Game.UnitTime * 2f;

        NavMeshAgent agent = GetT<NavMeshAgent>();
        agent.isStopped = false;

        Vector3 jumpPos = transform.position + (core.transform.position - transform.position).normalized * 3f;

        transform.DOLocalJump(jumpPos, 2, 1, jumpTime);

        await UniTask.Delay(TimeSpan.FromSeconds(jumpTime * 0.8f));

        if (gameObject.activeInHierarchy == false) return;

        Managers.Instance.Pool.PopObject(PoolType.CoreHitEffect, transform.position);
        core.Hit(HP);
        Die();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Core"))
        {
            _isLastJump = true;
        }
    }

    #endregion

    #region Hit

    public override void Stun(float time)
    {
        base.Stun(time);

        NavMeshAgent agent = GetT<NavMeshAgent>();
        agent.isStopped = true;
        agent.velocity = Vector3.zero;

        DOTween.Kill(_visual.gameObject);
        DOTween.Kill(gameObject);
    }

    public override void Die(InstrumentsTower attacker = null)
    {
        RemoveTarget();

        DOTween.Kill(gameObject);
        DOTween.Kill(_visual.gameObject);

        if (attacker != null)
        {
            Managers.Instance.Game.GetComponentInScene<MusicPowerData>().AddMusicPower(
                Managers.Instance.Data.EnemyDatas[Type].RewardMusicPower);
        }

        Managers.Instance.Game.KillCount++;

        base.Die(attacker);
    }

    #endregion
}
