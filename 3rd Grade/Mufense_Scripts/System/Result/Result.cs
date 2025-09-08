using UnityEngine;

public class Result : MonoBehaviour
{
    private void Start()
    {
        Managers.Instance.Pool.ResetPools();
    }
}
