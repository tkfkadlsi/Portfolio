using AYellowpaper.SerializedCollections;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Data/ConvertSO")]
public class ConvertDataSO : ScriptableObject
{
    [SerializedDictionary("EnemyType", "PoolType")] public SerializedDictionary<EnemyType, PoolType> EnemyType2PoolType = new SerializedDictionary<EnemyType, PoolType>();

    [SerializedDictionary("TowerType", "PoolType")] public SerializedDictionary<TowerType, PoolType> TowerType2PoolType = new SerializedDictionary<TowerType, PoolType>();

    [SerializedDictionary("TowerType", "KRText")] public SerializedDictionary<TowerType, string> TowerType2KRText = new SerializedDictionary<TowerType, string>();
}
