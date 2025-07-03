using AYellowpaper.SerializedCollections;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDataSO", menuName = "SO/Data/EnemyDataSO")]
public class EnemyDataSO : ScriptableObject
{
    [SerializedDictionary("Level", "HP")] public SerializedDictionary<int, float> EnemyHPDictionary = new SerializedDictionary<int, float>();

    public int RewardMusicPower;
}
