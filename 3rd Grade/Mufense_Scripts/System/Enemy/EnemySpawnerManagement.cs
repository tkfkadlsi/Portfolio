using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawnerManagement : MonoBehaviour
{
    private readonly float _spawnerLevelUpCooltime = 180f;
    private readonly float _enemyLevelUpCooltime = 180f;

    private float _spawnerLevelUpCooldown = 0f;
    private float _enemyLevelUpCooldown = 0f;

    private int _spawnerLevel = 0;
    private int _enemyLevel = 0;

    private List<EnemySpawner> _enemySpawnerList = new List<EnemySpawner>();
    private List<EnemyWayLine> _enemyWayLineList = new List<EnemyWayLine>();
    private Transform[] _spawnTrms;

    public int SpawnLevel
    {
        get
        {
            return _spawnerLevel;
        }
        set
        {
            _spawnerLevel = value;

            AddSpawner();
        }
    }

    public int EnemyLevel
    {
        get
        {
            return _enemyLevel;
        }
        set
        {
            _enemyLevel = value;

            foreach (EnemySpawner spawner in _enemySpawnerList)
            {
                spawner.SetEnemyLevel(_enemyLevel);
            }
        }
    }

    private void Awake()
    {
        Managers.Instance.Game.MusicPlayEvent += PlayMusicHandler;

        _spawnTrms = GetComponentsInChildren<Transform>().Where(x => (x != transform)).ToArray();

        _spawnerLevelUpCooldown = 90f;
    }

    private void Start()
    {
        Managers.Instance.UI.GetRootUI().GetCanvas<TextCanvas>().SetString(0, "스포너 개수 증가까지 남은 시간");
        Managers.Instance.UI.GetRootUI().GetCanvas<TextCanvas>().SetString(1, "적 HP 증가까지 남은 시간");
    }

    private void OnDestroy()
    {
        if (Managers.Instance != null)
        {
            Managers.Instance.Game.MusicPlayEvent -= PlayMusicHandler;
        }
    }

    private void Update()
    {
        _spawnerLevelUpCooldown += Time.deltaTime;
        _enemyLevelUpCooldown += Time.deltaTime;

        if (_spawnerLevelUpCooldown > _spawnerLevelUpCooltime)
        {
            _spawnerLevelUpCooldown -= _spawnerLevelUpCooltime;
            SpawnLevel++;
        }
        if (_enemyLevelUpCooldown > _enemyLevelUpCooltime)
        {
            _enemyLevelUpCooldown -= _enemyLevelUpCooltime;
            EnemyLevel++;
        }

        Managers.Instance.UI.GetRootUI().GetCanvas<TextCanvas>().SetTime(0, _spawnerLevelUpCooltime - _spawnerLevelUpCooldown);
        Managers.Instance.UI.GetRootUI().GetCanvas<TextCanvas>().SetTime(1, _enemyLevelUpCooltime - _enemyLevelUpCooldown);
    }

    private void PlayMusicHandler()
    {
        foreach (EnemySpawner spawner in _enemySpawnerList)
        {
            spawner.PushThisObject();
        }
        foreach (EnemyWayLine wayLine in _enemyWayLineList)
        {
            wayLine.PushThisObject();
        }

        _enemySpawnerList.Clear();
        _enemyWayLineList.Clear();
        Swap100();

        for (int i = 0; i < SpawnLevel + 1; i++)
        {
            _enemySpawnerList.Add(Managers.Instance.Pool.PopObject(PoolType.EnemySpawner, _spawnTrms[i].position).GetComponent<EnemySpawner>());
            _enemyWayLineList.Add(Managers.Instance.Pool.PopObject(PoolType.EnemyWayLine, _enemySpawnerList[i].transform.position).GetComponent<EnemyWayLine>());
            _enemyWayLineList[i].SettingLine();
        }
    }

    private void AddSpawner()
    {
        MusicPlayer musicPlayer = Managers.Instance.Game.GetComponentInScene<MusicPlayer>();
        MusicType type = musicPlayer.GetRandomMusic();
        musicPlayer.ChangeMusic(type);
    }

    private void Swap100()
    {
        for (int i = 0; i < 100; i++)
        {
            int swapNum = Random.Range(0, _spawnTrms.Length);
            int swapNum2 = Random.Range(0, _spawnTrms.Length);

            Transform dummy = _spawnTrms[swapNum];
            _spawnTrms[swapNum] = _spawnTrms[swapNum2];
            _spawnTrms[swapNum2] = dummy;
        }
    }
}
