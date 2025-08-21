using AYellowpaper.SerializedCollections;
using UnityEngine;
using System.Collections.Generic;

public class DataManager : MonoBehaviour
{


    [SerializedDictionary("Type", "DataSO")]
    public SerializedDictionary<EnemyType, EnemyDataSO> EnemyDatas = new SerializedDictionary<EnemyType, EnemyDataSO>();

    [field: SerializeField] public ConvertDataSO ConvertData { get; private set; }

    [field: SerializeField] public LayerMask EnemyLayer { get; private set; }

    [field: SerializeField] public TowerCountManagement TowerCountManagement { get; private set; } = new TowerCountManagement();

    [field: SerializeField] public TowerDataManagement TowerStatManagement { get; private set; } = new TowerDataManagement();

    [field: SerializeField] public List<Card> CardList { get; private set; } = new List<Card>();

    private void Awake()
    {
        TowerCountManagement.Init();
    }
}
