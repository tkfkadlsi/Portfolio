using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    #region Static

    private static int _enemySpawnCount = 0;
    private static int _spawnLevel => (int)(_enemySpawnCount * 0.001f);

    private static Dictionary<EnemyType, float> _enemySpawnRates = new Dictionary<EnemyType, float>()
    {
        { EnemyType.HalfEnemy, 0.04f },
        { EnemyType.QuarterEnemy, 0.95f },
        { EnemyType.QuarterRestEnemy, 0.01f }
    };
    private static List<EnemyType> _cashedSpawnRatesKey = new List<EnemyType>();

    private static Dictionary<EffectType, float> _enemyEffectRates = new Dictionary<EffectType, float>()
    {
        { EffectType.None, 1f },
        { EffectType.HighSpeed, 0f },
        { EffectType.Defend, 0f },
        { EffectType.Heal, 0f }
    };
    private static List<EffectType> _cashedEffectRatesKey = new List<EffectType>();

    #endregion

    private readonly float _equalizeSpeed = 0.001f;

    private readonly int _spawnCooltime = 4;
    private int _spawnCooldown;

    private void OnEnable()
    {
        Managers.Instance.Game.BeatEvent += BeatCounter;

        _cashedSpawnRatesKey = _enemySpawnRates.Keys.ToList();
        _cashedEffectRatesKey = _enemyEffectRates.Keys.ToList();

        _spawnCooldown = _spawnCooltime;
    }

    private void OnDisable()
    {
        if (Managers.Instance != null)
        {
            Managers.Instance.Game.BeatEvent -= BeatCounter;
        }
    }

    private void BeatCounter()
    {
        _spawnCooldown++;

        if(_spawnCooldown > _spawnCooltime)
        {
            _spawnCooldown -= _spawnCooltime;
            SpawnEnemy();
        }
    }
    
    private void SpawnEnemy()
    {
        EnemyType type = GetRandomEnemyType();
        EqualizeEnemySpawnRates();
        EffectType effectType = GetRandomEffectType();
        //EqualizeEnemyEffectRates();

        Enemy enemy = Managers.Instance.Pool.PopObject(Managers.Instance.Data.ConvertData.EnemyType2PoolType[type], transform.position).GetComponent<Enemy>();
        enemy.SetEffectType(effectType);
        enemy.SetHP(Managers.Instance.Data.EnemyDatas[type].EnemyHPDictionary[_spawnLevel]);
    }

    private EnemyType GetRandomEnemyType()
    {
        float totalRate = 0f;
        
        foreach(float rate in _enemySpawnRates.Values)
        {
            totalRate += rate;
        }

        float rand = Random.value * totalRate;
        float dummy = 0f;
        EnemyType type = EnemyType.QuarterEnemy;

        foreach(var kv in _enemySpawnRates)
        {
            dummy += kv.Value;

            if(rand <= dummy)
            {
                type = kv.Key;
                break;
            }
        }

        return type;
    }

    private void EqualizeEnemySpawnRates()
    {
        float avg = 0f;

        foreach (float value in _enemySpawnRates.Values)
        {
            avg += value;
        }

        avg /= _enemySpawnRates.Count;

        foreach(EnemyType type in _cashedSpawnRatesKey)
        {
            _enemySpawnRates[type] = Mathf.Lerp(_enemySpawnRates[type], avg, _equalizeSpeed);
        }
    }

    private EffectType GetRandomEffectType()
    {
        float totalRate = 0f;

        foreach (float rate in _enemyEffectRates.Values)
        {
            totalRate += rate;
        }

        float rand = Random.value * totalRate;
        float dummy = 0f;
        EffectType type = EffectType.None;

        foreach (var kv in _enemyEffectRates)
        {
            dummy += kv.Value;

            if (rand <= dummy)
            {
                type = kv.Key;
                break;
            }
        }

        return type;
    }

    private void EqualizeEnemyEffectRates()
    {
        float avg = 0f;

        foreach (float value in _enemyEffectRates.Values)
        {
            avg += value;
        }

        avg /= _enemyEffectRates.Count;

        foreach (EffectType type in _cashedEffectRatesKey)
        {
            _enemyEffectRates[type] = Mathf.Lerp(_enemyEffectRates[type], avg, _equalizeSpeed);
        }
    }
}