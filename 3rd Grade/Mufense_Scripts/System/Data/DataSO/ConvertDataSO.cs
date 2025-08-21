using AYellowpaper.SerializedCollections;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Data/ConvertSO")]
public class ConvertDataSO : ScriptableObject
{
    [SerializedDictionary("EnemyType", "PoolType")] public SerializedDictionary<EnemyType, PoolType> EnemyType2PoolType = new SerializedDictionary<EnemyType, PoolType>();

    [SerializedDictionary("TowerType", "PoolType")] public SerializedDictionary<TowerType, PoolType> TowerType2PoolType = new SerializedDictionary<TowerType, PoolType>();

    [SerializedDictionary("TowerType", "KRText")] public SerializedDictionary<TowerType, string> TowerType2KRText = new SerializedDictionary<TowerType, string>();

    [SerializedDictionary("TowerType", "CountCardType")] public SerializedDictionary<TowerType, CardType> TowerType2CountCardType = new SerializedDictionary<TowerType, CardType>();
    
    [SerializedDictionary("TowerType", "DamageCardType")] public SerializedDictionary<TowerType, CardType> TowerType2DamageCardType = new SerializedDictionary<TowerType, CardType>();

    [SerializedDictionary("TowerType", "RangeCardType")] public SerializedDictionary<TowerType, CardType> TowerType2RangeCardType = new SerializedDictionary<TowerType, CardType>();

    [SerializedDictionary("MusicType", "MusicCardType")] public SerializedDictionary<MusicType, CardType> MusicType2MusicCardType = new SerializedDictionary<MusicType, CardType>();
}
