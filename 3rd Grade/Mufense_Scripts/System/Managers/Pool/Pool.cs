using System.Collections.Generic;
using UnityEngine;

public class Pool
{
    public Transform PoolParent;
    private Queue<PoolableObject> _pool = new Queue<PoolableObject>();

    public int PoolCount => _pool.Count;

    public void Init(PoolType pooltype)
    {
        PoolParent = new GameObject().transform;
        PoolParent.SetParent(Managers.Instance.Pool.transform);
        PoolParent.name = pooltype.ToString();
    }

    public PoolableObject PopObject(Vector3 position)
    {
        PoolableObject po = _pool.Dequeue();
        po.transform.position = position;
        po.gameObject.SetActive(true);

        return po;
    }

    public PoolableObject PopObject(Transform parent, Vector3 position)
    {
        PoolableObject po = _pool.Dequeue();
        po.transform.SetParent(parent);
        po.transform.position = position;
        po.gameObject.SetActive(true);

        return po;
    }

    public void PushObject(PoolableObject po)
    {
        po.gameObject.SetActive(false);
        po.transform.SetParent(PoolParent);
        po.transform.position = new Vector3(0, -10000, 0);
        _pool.Enqueue(po);
    }

    public void ResetPool(PoolManager pm)
    {
        while(_pool.Count > 0)
        {
            pm.DestroyObject(_pool.Dequeue());
        }
    }
}
