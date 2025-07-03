using AYellowpaper.SerializedCollections;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerDataSO", menuName = "SO/Data/TowerDataSO")]
public class TowerDataSO : ScriptableObject
{
    public string DisplayName = "";

    [SerializedDictionary("Level", "Damage")]
    public SerializedDictionary<int, float> Damage = new SerializedDictionary<int, float>();
    
    [SerializedDictionary("Level", "Range")]
    public SerializedDictionary<int, float> Range = new SerializedDictionary<int, float>();

    [SerializedDictionary("Level", "MusicPower")]
    public SerializedDictionary<int, int> UsingMusicPower = new SerializedDictionary<int, int>();
}
