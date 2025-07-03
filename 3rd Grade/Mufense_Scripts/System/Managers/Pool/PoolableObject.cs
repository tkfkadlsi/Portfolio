using UnityEngine;

public class PoolableObject : MonoBehaviour
{
    private PoolType _poolType;

    public void SetPoolType(PoolType poolType)
    {
        _poolType = poolType;
    }

    public void PushThisObject()
    {
        Managers.Instance.Pool.PushObject(_poolType, this);
    }
}