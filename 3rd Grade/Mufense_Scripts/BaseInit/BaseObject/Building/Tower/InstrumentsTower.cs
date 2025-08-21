using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class InstrumentsTower : Tower, IPointerClickHandler
{

    [SerializeField] protected Vector3 _offset;

    protected override void Init()
    {
        base.Init();
    }

    protected override void Enable()
    {
        base.Enable();

        _stunEffect = Managers.Instance.Pool.PopObject(PoolType.TowerStunEffect, transform.position).GetComponent<TowerStunEffect>();
        _stunEffect.transform.localScale = Vector3.one * 2;
    }

    protected override void Disable()
    {
        if(_stunEffect != null)
        {
            _stunEffect.PushThisObject();
            _stunEffect = null;
        }

        base.Disable();
    }

    protected override void BeatHandler()
    {
        base.BeatHandler();

        _stunBeat--;

        if(_stunBeat <= 0)
        {
            _stunEffect.OffEffect();
            _stunEffect.gameObject.SetActive(false);
            _stunEffect.gameObject.SetActive(true);
        }
    }

    #region Rotate

    private float _rotateSpeed = 5f;

    private void Update()
    {
        if (_target != null && _target.gameObject.activeInHierarchy)
        {
            Vector3 direction = _target.transform.position - transform.position;
            direction.y = 0;

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, _rotateSpeed * Time.deltaTime);
        }
    }

    #endregion

    #region Target

    protected Enemy _target;
    protected int _count;
    protected Collider[] _buffer = new Collider[20];

    protected virtual void FindTarget()
    {
        _count = Physics.OverlapSphereNonAlloc(transform.position, _range, _buffer, Managers.Instance.Data.EnemyLayer);
    }

    #endregion

    #region Attack

    protected float _range
    {
        get
        {
            return Managers.Instance.Data.TowerStatManagement.GetRange(Type, Level);
        }
    }
    protected float _damage
    {
        get
        {
            float damage = Managers.Instance.Data.TowerStatManagement.GetDamage(Type);

            float multiplier = 1.0f;

            foreach (float f in _damageMultipliers)
            {
                multiplier += f;
            }

            return damage * multiplier;
        }
    }

    private List<float> _damageMultipliers = new List<float>();

    public void AddDamageMultiplier(float multiplier, float time)
    {
        StartCoroutine(AddMultiplierCoroutine(multiplier, time));
    }

    private IEnumerator AddMultiplierCoroutine(float multiplier, float time)
    {
        _damageMultipliers.Add(multiplier);

        yield return Managers.Instance.Game.GetWaitForSeconds(time);

        if (_damageMultipliers.Contains(multiplier))
        {
            _damageMultipliers.Remove(multiplier);
        }
    }

    protected abstract void InstrumentsHandler(bool isHigh);

    #endregion

    #region Stun

    protected int _stunBeat { get; private set; }
    private TowerStunEffect _stunEffect;

    public void Stun(int stunBeat)
    {
        stunBeat *= 4;

        if (stunBeat > _stunBeat)
        {
            _stunBeat = stunBeat;

            Vector3 offset = new Vector3(0, 2, 0);
            _stunEffect.SettingPosition(transform.position + offset);

            _stunEffect.OnEffect();
        }
    }

    #endregion

    #region Click

    public void OnPointerClick(PointerEventData eventData)
    {
        Managers.Instance.Game.GetComponentInScene<CameraRoot>().SetPosition(transform.position);
        Managers.Instance.UI.GetRootUI().GetCanvas<MenuCanvas>().SetEnable(false);
        Managers.Instance.UI.GetRootUI().GetCanvas<TowerInfoCanvas>().SetTower(this);
    }

    #endregion
}