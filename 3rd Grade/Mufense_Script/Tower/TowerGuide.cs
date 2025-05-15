using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TowerGuide : BaseObject, IMusicPlayHandle
{

    private TowerSpawner _towerSpawner;
    private Rigidbody2D _rigidbody;
    private Collider2D _collider;
    private TextMeshProUGUI _text;

    private List<Collider2D> _colliders = new List<Collider2D>();

    private IEnumerator BuildCor;
    private bool _canBuild;

    private bool _isOverlap;

    protected override bool Init()
    {
        if (base.Init() == false)
        {
            return false;
        }

        _towerSpawner = Managers.Instance.Game.FindBaseInitScript<TowerSpawner>();
        _text = gameObject.FindChild<TextMeshProUGUI>("GuideText", true);

        _objectType = ObjectType.TowerGuide;
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _canBuild = true;
        _isOverlap = false;

        return true;
    }

    protected override void Setting()
    {
        base.Setting();

        _rigidbody.gravityScale = 0f;
        _collider.isTrigger = true;
    }

    private void Update()
    {

        if (_isOverlap)
        {
            _spriteRenderer.color = Color.red;
            _canBuild = false;
        }
        else
        {
            _spriteRenderer.color = Color.green;
            _canBuild = true;
        }

        if (Input.GetMouseButtonDown(1) && BuildCor is not null)
        {
            StopCoroutine(BuildCor);
            _towerSpawner.SetSpawnState(TowerSpawnState.None, TowerType.None, 0);
            Managers.Instance.UI.GameRootUI.MainCanvas.SetBuildButtonActive(true);
        }
    }

    public void BuildTower(TowerType type, int cost)
    {
        BuildCor = BuildCoroutine(type, cost);
        StartCoroutine(BuildCor);
    }

    private IEnumerator BuildCoroutine(TowerType type, int cost)
    {
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0) && _canBuild);
        Vector3 buildPos = transform.position;

        Managers.Instance.Game.FindBaseInitScript<MusicPowerChest>().RemoveMusicPower(cost);
        _towerSpawner.SetSpawnState(TowerSpawnState.None, TowerType.None, 0);

        Tower newTower;

        switch (type)
        {
            case TowerType.Piano:
                newTower = Managers.Instance.Pool.PopObject(PoolType.PianoTower, buildPos).GetComponent<Tower>();
                Managers.Instance.UI.GameRootUI.BuildingsCanvas.PianoCost *= 2;
                break;
            case TowerType.Drum:
                newTower = Managers.Instance.Pool.PopObject(PoolType.DrumTower, buildPos).GetComponent<Tower>();
                Managers.Instance.UI.GameRootUI.BuildingsCanvas.DrumCost *= 2;
                break;
            case TowerType.String:
                newTower = Managers.Instance.Pool.PopObject(PoolType.StringTower, buildPos).GetComponent<Tower>();
                Managers.Instance.UI.GameRootUI.BuildingsCanvas.StringCost *= 2;
                break;
        }

        Managers.Instance.UI.GameRootUI.MainCanvas.SetBuildButtonActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Tower") || collision.CompareTag("Core") || collision.CompareTag("Way"))
        {
            _colliders.Add(collision);

            if (_colliders.Count == 0)
            {
                _isOverlap = false;
            }
            else
            {
                _isOverlap = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_colliders.Contains(collision))
        {
            _colliders.Remove(collision);

            if (_colliders.Count == 0)
            {
                _isOverlap = false;
            }
            else
            {
                _isOverlap = true;
            }
        }
    }

    public void SettingColor(Music music)
    {
        _text.color = music.TextColor;
    }
}
