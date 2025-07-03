using AYellowpaper.SerializedCollections;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    [SerializedDictionary("Type", "DataSO")]
    public SerializedDictionary<TowerType, TowerDataSO> TowerDatas = new SerializedDictionary<TowerType, TowerDataSO>();

    [SerializedDictionary("Type", "DataSO")]
    public SerializedDictionary<EnemyType, EnemyDataSO> EnemyDatas = new SerializedDictionary<EnemyType, EnemyDataSO>();

    [field: SerializeField] public ConvertDataSO ConvertData { get; private set; }

    [field: SerializeField] public LayerMask EnemyLayer { get; private set; }
}
