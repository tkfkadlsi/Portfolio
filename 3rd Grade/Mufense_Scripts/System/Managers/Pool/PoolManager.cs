using AYellowpaper.SerializedCollections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [SerializedDictionary("Pool", "Object")] public SerializedDictionary<PoolType, PoolableObject> PoolableObjectDictionary = new SerializedDictionary<PoolType, PoolableObject>();

    private Dictionary<PoolType, Pool> _poolDictionary = new Dictionary<PoolType, Pool>();

    private void Awake()
    {
        foreach(PoolType type in PoolableObjectDictionary.Keys)
        {
            _poolDictionary.Add(type, new Pool());
            _poolDictionary[type].Init(type);
        }
    }

    private PoolableObject CreateObject(PoolType type)
    {
        PoolableObject po = Instantiate(PoolableObjectDictionary[type]);
        po.SetPoolType(type);

        po.gameObject.SetActive(false);
        po.transform.position = new Vector3(0, -10000f, 0);
        return po;
    }

    public PoolableObject PopObject(PoolType type, Vector3 position)
    {
        PoolableObject po;

        if(_poolDictionary[type].PoolCount == 0)
        {
            po = CreateObject(type);
            po.transform.position = position;
            po.gameObject.SetActive(true);
        }
        else
        {
            po = _poolDictionary[type].PopObject(position);
        }

        return po;
    }

    public PoolableObject PopObject(PoolType type, Transform parent, Vector3 position)
    {
        PoolableObject po;

        if (_poolDictionary[type].PoolCount == 0)
        {
            po = CreateObject(type);
            po.transform.position = position;
            po.transform.SetParent(parent);
            po.gameObject.SetActive(true);
        }
        else
        {
            po = _poolDictionary[type].PopObject(parent, position);
        }

        return po;
    }

    public void PushObject(PoolType type, PoolableObject po)
    {
        _poolDictionary[type].PushObject(po);
    }
}